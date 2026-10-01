using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float smoothTime = 0.2f;
    private Vector3 velocity;
    private Vector3 offset = new Vector3(0, 7, -5);

    void Start()
    {
        if (target == null)
            return;
        transform.position = target.position + offset;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position + offset,
            ref velocity,
            smoothTime
        );

    }

}