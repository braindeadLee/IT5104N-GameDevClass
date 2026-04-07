using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct LevelData
{
    public int numToSpawnMin;
    public int numToSpawnMax;

    public float speedMin;
    public float speedMax;

    public float cooldownMin;
    public float cooldownMax;

    public float levelTime;

    public string levelName;
}
public class SpikeManager : MonoBehaviour
{
    [SerializeField] public LevelData[] levelData;
    private int levelAmount;
    private int levelCounter = 0;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] GameObject spikePrefab;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timerText;
    private int numToSpawn = 0;
    private float longCooldown = 3f;
    private float speed = 3f;
    private bool canSpawn = true;
    private bool gameOver = false;
    private float shortCoolDown = 0.2f;

        void OnEnable()
    {
        PlayerHealth.gotKilled += ClearSpikes;
        PlayerHealth.gotKilled += EarlyGameOver;
    }

    void OnDisable()
    {
        PlayerHealth.gotKilled -= ClearSpikes;
        PlayerHealth.gotKilled -= EarlyGameOver;
    }

    private void Start()
    {   
        gameOver = false;

        levelText.text = string.Empty;
        timerText.text = string.Empty;


        levelCounter = 0;
        levelAmount = levelData.Length;
        Debug.Log($"Level Amount: {levelAmount}");
        StartCoroutine(NextWave());
    }
    private void Update()
    {
        if (canSpawn && !gameOver)
        {
            numToSpawn = SpawnNextSpike(numToSpawn);
        }
    }

    private IEnumerator NextWave()
    {
        if(levelCounter >= levelAmount)
        {
            levelText.text = "All levels completed!";
            timerText.text = ":)";
            Debug.Log("All levels completed!");
            gameOver = true;
            yield break;
        } else
        {
            levelText.text = levelData[levelCounter].levelName;      

            SetNextNumToSpawn();  

            float timer = 0f;
            while (timer < levelData[levelCounter].levelTime)
            {

                timer += Time.deltaTime;
                timerText.text = $"Time: {Mathf.RoundToInt(levelData[levelCounter].levelTime - timer)}s";
                yield return null;
            }

            levelCounter++;
            StartCoroutine(NextWave());
        }
    }

    private int SpawnNextSpike(int queue)
    {
        if (numToSpawn <= 0)
        {

            Invoke(nameof(SetNextNumToSpawn), longCooldown);
            canSpawn = false;
            Invoke(nameof(EnableSpawn), longCooldown);
            return 0;
        }
        else
        {
            GameObject spike = Instantiate(spikePrefab, startPoint.position, Quaternion.identity);
            StartCoroutine(spike.GetComponent<Spike>().moveSpike(speed, startPoint.position, endPoint.position));

            canSpawn = false;
            Invoke(nameof(EnableSpawn), shortCoolDown);

            return queue - 1;
    }
        }
        
    private void EnableSpawn() => canSpawn = true;
    private void SetNextNumToSpawn()
    {
        int minSpawn = levelData[levelCounter].numToSpawnMin;
        int maxSpawn = levelData[levelCounter].numToSpawnMax;
        numToSpawn = UnityEngine.Random.Range(minSpawn, maxSpawn);

        float minCooldown = levelData[levelCounter].cooldownMin;
        float maxCooldown = levelData[levelCounter].cooldownMax;
        longCooldown = UnityEngine.Random.Range(minCooldown, maxCooldown);

        float minSpeed = levelData[levelCounter].speedMin;
        float maxSpeed = levelData[levelCounter].speedMax;
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    private void ClearSpikes()
    {
        Spike[] spikes = FindObjectsByType<Spike>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (Spike spike in spikes)
        {
            Destroy(spike.gameObject);
        }
    }

    private void EarlyGameOver()
    {
            ClearSpikes();
            StopAllCoroutines();
            levelText.text = "Game Over!";
            timerText.text = ":(";
            Debug.Log("Game Over!");
            canSpawn = false;
            gameOver = true;
    }
}