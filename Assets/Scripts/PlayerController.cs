using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public partial class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private InputActionAsset playerActions;
    public static PlayerController Instance;

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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions = playerActions;
        playerInput.defaultActionMap = "PlayerActionMap";
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
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        }
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerInput.ActivateInput();
        rb.WakeUp();
    }
}