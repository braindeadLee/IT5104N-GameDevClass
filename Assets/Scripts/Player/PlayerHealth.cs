using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private float iSeconds = 2f;
    [SerializeField] private bool invincible = false;
    [SerializeField] private float hurtEffectTime = 0.5f;

    SpriteRenderer sr;
    public static event Action<float, Vector2> gotHurt;
    public static PlayerHealth Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }

        sr = GetComponent<SpriteRenderer>();
    }

    public void Hurt(int damage, float knockbackForce, Vector2 enemyPosition)
    {
        if (!invincible)
        {
            health -= damage;
            gotHurt?.Invoke(knockbackForce, enemyPosition);
            StartCoroutine(InvincibleMode());
            StartCoroutine(HurtEffect());
        }
    }

    private IEnumerator InvincibleMode()
    {
        invincible = true;
        yield return new WaitForSeconds(iSeconds);
        invincible = false;
    }

    private IEnumerator HurtEffect()
    {
        Color originalColor = sr.color;
        sr.color = Color.red;

        yield return new WaitForSeconds(hurtEffectTime);
        sr.color = originalColor;
    }
}
