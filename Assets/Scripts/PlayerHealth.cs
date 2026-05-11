using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    [Header("Damage Effects")]
    [SerializeField] private float iFramesDuration = 1.0f; // Segundos invulnerabilidad
    [SerializeField] private float flashDelay = 0.1f; // Velocidad parpadeo
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
        {
            Die();
        }
        if (!isInvulnerable)
        {
            currentHealth -= amount;

            // 5. Llama al efecto visual de parpadeo e invulnerabilidad
            StartCoroutine(DamageEffectsRoutine());

            // Aquí iría el resto de tu código anterior (ej. actualizar barra de vida)
            Debug.Log("Vida restante: " + currentHealth);
        }
    }
    public void Die()
    {
        Debug.Log("The player has died");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}
