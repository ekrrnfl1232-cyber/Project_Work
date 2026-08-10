using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector3 movement;

    private float jumpment = 3f;
    Rigidbody rb;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * Time.deltaTime * 3f);
    }

    public void OnMove(InputValue value)
    {
        Vector2 vec2= value.Get<Vector2>();
        movement = new Vector3(vec2.x, 0f, vec2.y).normalized;
    }
    public void OnJump(InputValue value)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * jumpment, ForceMode.Impulse);
    }
}
