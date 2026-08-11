using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;



public class PlayerController : MonoBehaviour
{
    private float moveForce = 2f;

    private float jumpForce = 4f;

    private float dashForce = 20f;

    Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Look()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = worldMousePos - transform.position;
        direction.y = 0f;

        transform.forward = direction;
    }

    public void OnAttack(InputValue value)
    {
        Debug.Log("АјАн!");
    }

    public void OnDash(InputValue shift)
    {
        rb.AddRelativeForce(Vector3.forward * dashForce, ForceMode.VelocityChange);
        Invoke("StopDash", 0.2f);
    }
    void StopDash()
    {
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
    }
    public void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(hor, 0, ver).normalized;

        rb.MovePosition(rb.position + moveDir * moveForce * Time.deltaTime);
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector3(0f, Mathf.Sqrt(2f * 9.81f * jumpForce), 0f);
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            Move();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        Look();
    }
}
