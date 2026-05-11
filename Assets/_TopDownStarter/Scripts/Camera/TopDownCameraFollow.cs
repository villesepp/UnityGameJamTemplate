using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow")]
    [SerializeField] private float smoothTime = 0.15f;

    private Vector3 velocity;

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}