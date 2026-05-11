using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 3f;
    private Rigidbody2D rb;
    private float minDistance = 0.1f; // Reducido para asegurar el contacto

    [SerializeField] private float detectionDistance = 1.5f;
    [SerializeField] private LayerMask obstacleLayer;

    private Vector2 chosenAvoidanceDir;
    private float avoidanceTimer;
    private float avoidanceStickiness = 0.2f;

    [Header("Attack Settings")]
    [SerializeField] private int damageAmount = 10;
    private bool hasAttacked = false; //Evito que haga daño 2 veces

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 dirToTarget = (target.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.position);

        // Persecución constante
        if (distance > minDistance)
        {
            Vector2 finalDir = CalculateSmartDirection(dirToTarget);
            rb.linearVelocity = finalDir * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private Vector2 CalculateSmartDirection(Vector2 targetDir)
    {
        if (avoidanceTimer > 0)
        {
            avoidanceTimer -= Time.fixedDeltaTime;
            return chosenAvoidanceDir;
        }

        // Detección de obstáculos (Raycast circular)
        RaycastHit2D hitCenter = Physics2D.CircleCast(transform.position, 0.3f, targetDir, detectionDistance, obstacleLayer);

        if (hitCenter.collider != null)
        {
            // Intentar esquivar por los lados si hay un obstáculo
            Vector2 leftRayDir = Quaternion.Euler(0, 0, 45) * targetDir;
            Vector2 rightRayDir = Quaternion.Euler(0, 0, -45) * targetDir;

            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, leftRayDir, detectionDistance, obstacleLayer);
            if (hitLeft.collider == null)
            {
                chosenAvoidanceDir = leftRayDir;
                avoidanceTimer = avoidanceStickiness;
                return leftRayDir;
            }

            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rightRayDir, detectionDistance, obstacleLayer);
            if (hitRight.collider == null)
            {
                chosenAvoidanceDir = rightRayDir;
                avoidanceTimer = avoidanceStickiness;
                return rightRayDir;
            }
        }

        return targetDir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifico si ya atacó para no repetir
        if (hasAttacked) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

            if (player != null)
            {
                //Bloqueamos futuros ataques
                hasAttacked = true;

                player.TakeDamage(damageAmount);

                // El enemigo se destruye
                Destroy(gameObject);
            }
        }
    }
}
