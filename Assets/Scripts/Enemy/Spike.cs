using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackForce = 3f;

    PlayerHealth playerHealth;

    void Start()

    {
        playerHealth = PlayerHealth.Instance;
    }

    public void Attack()
    {
        playerHealth.Hurt(damage, knockbackForce, transform.position);
    }

    void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            Debug.Log("Hit");
            Attack();
        }
    }

    public void ActivateMovement()
    {
        
    }

    public IEnumerator moveSpike(float time, Vector2 startPoint, Vector2 endPoint)
    {
        float timer = 0f;
        while(timer < time)
        {
            transform.position = Vector2.Lerp(startPoint, endPoint, timer/time);

            timer += Time.deltaTime;
            yield return null;
        }
        destroySpike();
    }

    private void destroySpike()
    {
        Destroy(gameObject);
    }
    
}
