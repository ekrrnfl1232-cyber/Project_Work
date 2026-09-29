using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float smoothTime = 0.2f;
    private Vector2 velocity;

    void Start()
    {
        if (target == null)
            return;
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }

    private void LateUpdate()
    {
        Vector2 next = Vector2.SmoothDamp(
            transform.position,
            target.position,
            ref velocity,
            smoothTime
        );

        transform.position = new Vector3(next.x, next.y, -10);
    }

}
