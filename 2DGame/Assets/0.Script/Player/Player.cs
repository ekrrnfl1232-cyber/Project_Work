using UnityEngine;

public class Player : MonoBehaviour
{

    private float moveSpeed = 10f;

    public Rigidbody2D rb;

    public Animator anim;

    void Update()
    {

        Vector2 moveDir = InputManager.Instance.input.Player.Move.ReadValue<Vector2>();
        anim.SetFloat("DirX", moveDir.x);
        anim.SetFloat("DirY", moveDir.y);
        if (InputManager.Instance.input.Player.Move.IsPressed())
        {
            anim.SetTrigger("Run");
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
        else if(!InputManager.Instance.input.Player.Move.IsPressed())
        {
            anim.SetTrigger("Idle");
        }

    }
}
