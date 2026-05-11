using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChaserEnemy : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private string targetTag = "Player";

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (target == null)
        {
            GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

            if (targetObject != null)
            {
                target = targetObject.transform;
            }
            else
            {
                Debug.LogWarning($"{gameObject.name} could not find target with tag {targetTag}.");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!CanMove())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MoveTowardTarget();
    }

    private bool CanMove()
    {
        if (target == null)
            return false;

        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentState != GameState.Playing)
            return false;

        return true;
    }

    private void MoveTowardTarget()
    {
        Vector2 currentPosition = rb.position;
        Vector2 targetPosition = target.position;

        Vector2 direction = targetPosition - currentPosition;

        if (direction.magnitude <= stoppingDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = direction.normalized * moveSpeed;
    }
}