using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownPlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.action.Disable();
        }
    }

    private void Update()
    {
        ReadMovementInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadMovementInput()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentState != GameState.Playing)
        {
            moveInput = Vector2.zero;
            return;
        }

        if (moveAction == null)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentState != GameState.Playing)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = moveInput * moveSpeed;
    }
}