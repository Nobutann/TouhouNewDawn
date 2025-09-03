using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxLives = 3;
    public float invulnerabilityTime = 2f;

    [Header("Visual Feedback")]
    public float blinkSpeed = 0.1f;

    private int currentLives;
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;

    void Start()
    {
        currentLives = maxLives;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;

            HandleBlinking();

            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
                spriteRenderer.color = Color.white;
            }
        }
    }

    void HandleBlinking()
    {
        if (Time.time % (blinkSpeed * 2) < blinkSpeed)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInvulnerable && other.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentLives--;

        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityTime;

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public void AddLife()
    {
        currentLives++;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        currentLives = maxLives;
        isInvulnerable = false;
        spriteRenderer.color = Color.white;
    }
}