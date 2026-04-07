using System;
using UnityEngine;

public class RunnerEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int damage = 2;
    [SerializeField] private float knockbackForce = 3f;

    float scaleX;    
    Rigidbody2D rb;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;

    void Start()

    {
        playerMovement = PlayerMovement.Instance;
        playerHealth = PlayerHealth.Instance;

        rb = GetComponent<Rigidbody2D>();

        scaleX = transform.localScale.x;
    }

    void FixedUpdate()
    {
        if (PlayerMovement.Instance != null)
        {
            float directionX = playerMovement.transform.position.x - transform.position.x;
            rb.linearVelocityX = Mathf.Sign(directionX) * speed;

            handleDirection();
        }
        
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

    private void handleDirection()
    {
        if(rb.linearVelocityX == 0)
        {
            return;
        }

        float targetScaleX = (rb.linearVelocityX > 0) ? scaleX : -scaleX;
        transform.localScale = new Vector2(targetScaleX, transform.localScale.y);
    }

}
