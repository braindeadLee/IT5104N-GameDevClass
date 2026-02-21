using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player : ScriptableObject
{
    public int health;
    public int armor;
    public float moveSpeed;
    public float jumpForce;
    
}
