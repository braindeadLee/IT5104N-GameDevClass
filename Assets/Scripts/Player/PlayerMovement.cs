using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    private float xVal;

    [Header("Jump")]

    [SerializeField] private float jumpForce = 5f;
    private bool jump = false;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundToucher;
    private float toucherRadius = 0.2f;

    private float scaleX;
    private bool isKnockedBack = false;

    public static PlayerMovement Instance { get; private set; }
    public PlayerHealth playerHealth;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;

    }

    void OnEnable()
{
    PlayerHealth.gotHurt += TakeKnockback;
}

void OnDisable()
{
    PlayerHealth.gotHurt -= TakeKnockback;
}

    void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            handleDirection();

            Vector2 currentVelocity = rb. linearVelocity;

            currentVelocity.x = xVal * speed;

            rb.linearVelocity = currentVelocity;
        }
    
        if (jump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
            
            jump = false;
        }
    }
    private bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundToucher.position, toucherRadius, groundLayer)) 
            return true;

        return false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        xVal = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && context.started) jump = true;
    }

    private void handleDirection()
    {
        if(xVal == 0)
        {
            return;
        }

        float targetScaleX = (xVal > 0) ? scaleX : -scaleX;

        transform.localScale = new Vector2(targetScaleX, transform.localScale.y);
    }

    private void TakeKnockback(float knockbackForce, Vector2 enemyPosition)
    {
        isKnockedBack = true;

        Debug.Log($"Knocked back with force of :{knockbackForce}");
        Vector2 direction = ((Vector2)transform.position - enemyPosition).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse); 

        Invoke(nameof(ResetKnockback), 0.2f);
    }

    private void ResetKnockback()
    {
        isKnockedBack = false;
    }
}