using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 8f;
    [SerializeField] private float JumpForce = 8f;

    private float initialScaleX;
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalInput;
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

    // Se activa con el componente Player Input (Action: Move)
    public void OnMove(InputValue value)
    {
        horizontalInput = value.Get<Vector2>().x;
    }

    // Se activa con el componente Player Input (Action: Jump)
    public void OnJump(InputValue value)
    {
        // En el sistema de mensajes, comprobamos si el botón fue presionado
        if (value.isPressed && isOnGround)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Asegúrate de que tu suelo tenga el Tag "Suelo" en el Inspector
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isOnGround = true;
        }
    }

    void FixedUpdate()
    {
        // Movimiento
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Animación (Recuerda crear el parámetro "Speed" tipo Float en el Animator)
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        }

        // Girar Sprite
        if (horizontalInput > 0)
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, transform.localScale.z);
    }
}