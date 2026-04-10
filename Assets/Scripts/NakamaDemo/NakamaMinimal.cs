using System.Collections.Generic;
using System.Text;
using Nakama;
using UnityEngine;
using UnityEngine.InputSystem; // <-- Required for the new Input System!

public class NakamaMinimal : MonoBehaviour
{
    public GameObject playerPrefab;
    
    private IClient client;
    private ISession session;
    private ISocket socket;
    private string currentMatchId;
    
    // Local player and a dictionary to track the other guy
    private GameObject localPlayer;
    private Dictionary<string, GameObject> remotePlayers = new Dictionary<string, GameObject>();

    // Bare minimum movement variables
    private float moveSpeed = 5f;
    private const long MOVEMENT_OPCODE = 1;

    async void Start()
    {
        client = new Client("http", "127.0.0.1", 7350, "defaultkey");

        // Generates a random, completely unique string of letters and numbers every time the game boots
        var deviceId = System.Guid.NewGuid().ToString();
        session = await client.AuthenticateDeviceAsync(deviceId);
        Debug.Log("Authenticated! Session: " + session.UserId);

        socket = client.NewSocket();
        await socket.ConnectAsync(session);

        socket.ReceivedMatchmakerMatched += OnMatchmakerMatched;
        socket.ReceivedMatchState += OnReceivedMatchState;

        await socket.AddMatchmakerAsync("*", 2, 2);
        Debug.Log("Looking for match...");
    }

    private async void OnMatchmakerMatched(IMatchmakerMatched matched)
    {
        var match = await socket.JoinMatchAsync(matched);
        currentMatchId = match.Id;
        Debug.Log("Match Joined! ID: " + currentMatchId);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            localPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            localPlayer.GetComponent<SpriteRenderer>().color = Color.blue; 
        });
    }

    void Update()
    {
        if (localPlayer != null && !string.IsNullOrEmpty(currentMatchId))
        {
            float x = 0f;
            float y = 0f;

            // NEW: Read the WASD keys directly using the new Input System
            if (Keyboard.current != null)
            {
                if (Keyboard.current.dKey.isPressed) x += 1f;
                if (Keyboard.current.aKey.isPressed) x -= 1f;
                if (Keyboard.current.wKey.isPressed) y += 1f;
                if (Keyboard.current.sKey.isPressed) y -= 1f;
            }

            if (x != 0 || y != 0)
            {
                Vector3 move = new Vector3(x, y, 0).normalized * moveSpeed * Time.deltaTime;
                localPlayer.transform.position += move;

                SendPosition(localPlayer.transform.position);
            }
        }
    }

    private void SendPosition(Vector3 pos)
    {
        string json = $"{{\"x\":{pos.x}, \"y\":{pos.y}}}";
        socket.SendMatchStateAsync(currentMatchId, MOVEMENT_OPCODE, json);
    }

    private void OnReceivedMatchState(IMatchState state)
    {
        if (state.OpCode == MOVEMENT_OPCODE)
        {
            string json = Encoding.UTF8.GetString(state.State);
            var posData = JsonUtility.FromJson<PositionData>(json);
            Vector3 newPos = new Vector3(posData.x, posData.y, 0);

            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                if (!remotePlayers.ContainsKey(state.UserPresence.SessionId))
                {
                    GameObject remote = Instantiate(playerPrefab, newPos, Quaternion.identity);
                    remote.GetComponent<SpriteRenderer>().color = Color.red; 
                    remotePlayers.Add(state.UserPresence.SessionId, remote);
                }
                else
                {
                    remotePlayers[state.UserPresence.SessionId].transform.position = newPos;
                }
            });
        }
    }

    [System.Serializable]
    private class PositionData
    {
        public float x;
        public float y;
    }
}