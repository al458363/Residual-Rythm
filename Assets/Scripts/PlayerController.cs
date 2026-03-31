using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 8f;

    private float initialScaleX;

    private Rigidbody2D rb;
    private Animator animator;

    private float horizontalInput;

    //Mio
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private float JumpForce = 8f;
    private bool isOnGround;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        jumpAction.action.Enable();
        jumpAction.action.performed += OnJump;
    }
    void OnDisable()
    {
        jumpAction.action.Disable();
        jumpAction.action.performed -= OnJump;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isOnGround)
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Guardamos la escala que le pusiste en el Inspector al empezar
        initialScaleX = transform.localScale.x;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Se activa con el componente Player Input (Action: Move)
    public void OnMove(InputValue value)
    {
        // En plataformas solo nos interesa el eje X (A/D)
        horizontalInput = value.Get<Vector2>().x;

        // Esto imprimirá los valores en la consola cada vez que presiones WASD
        //Debug.Log("Movimiento detectado: " + moveInput);
    }

    void FixedUpdate()
    {
        // Movimiento horizontal manteniendo la velocidad vertical (gravedad/salto)
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Actualizar Animación (usamos Mathf.Abs para que sea siempre positivo)
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Girar el Sprite según la dirección
        if (horizontalInput > 0)
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, transform.localScale.z); // Mira a la derecha
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);  // Mira a la izquierda
    }

    // Update is called once per frame
    void Update()
    {
    }
}
