using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public bool isDead = false;
    [Header("Health")]
    public HealthData stats;
    public event Action<float, float> OnHealthChanged;
    public static PlayerHealth Instance;
    [Header("Damage Effects")]
    [SerializeField] private float iFramesDuration = 1.0f; // Segundos invulnerabilidad
    [SerializeField] private float flashDelay = 0.1f; // Velocidad parpadeo
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats.currentHealth = stats.maxHealth;
        OnHealthChanged?.Invoke(stats.currentHealth , stats.maxHealth);
    }
    public void TakeDamage(int amount)
    {
        if (!isInvulnerable && !isDead)
        {
            stats.currentHealth -= amount;
            if (stats.currentHealth < 0)
            {
                stats.currentHealth = 0;
            }
            OnHealthChanged?.Invoke(stats.currentHealth, stats.maxHealth);
            StartCoroutine(DamageEffectsRoutine());
            if (stats.currentHealth <= 0)
            {
                Die();
            }
        }
    }
    public void Die()
    {
        isDead = true;
        Invoke("RestartGame", 0.2f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator DamageEffectsRoutine()
    {
        isInvulnerable = true;

        int flashes = Mathf.RoundToInt(iFramesDuration / (flashDelay * 2));

        if (flashes <= 0) flashes = 3;

        for (int i = 0; i < flashes; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDelay);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDelay);
        }

        isInvulnerable = false; // Fin del efecto
    }
    public void RestartGame()
    {
        ResetStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnHealthChanged?.Invoke(stats.currentHealth, stats.maxHealth);
    }
    public void ResetStats()
    {
        isDead = false;
        stats.currentHealth = stats.maxHealth;
        isInvulnerable = false;
        spriteRenderer.color = Color.white;

    }

}
