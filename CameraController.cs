using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(0f, 5, -6);
    }
}
