using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 8f;
    [SerializeField] private float JumpForce = 8f;

    private float initialScaleX;
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalInput = 1f;
    private bool isOnGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        initialScaleX = transform.localScale.x;
    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed && isOnGround)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isOnGround = true;
        }
    }

    void FixedUpdate()
    {
        // 3. El movimiento ahora siempre usará el -1f definido arriba
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if (animator != null)
        {
            // La animación se mantendrá activa porque Abs(-1) es 1
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        }

        // 4. Girar Sprite: Al ser siempre negativo, se mantendrá mirando a la izquierda
        if (horizontalInput > 0)
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, transform.localScale.z);
    }
}