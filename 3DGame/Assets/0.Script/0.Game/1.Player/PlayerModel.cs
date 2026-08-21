using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private PlayerView view;
    public float MoveForce{ get;set; }

    public float JumpForce{ get;set; }

    public float DashForce { get; set; }

    public float InterationScale { get; set; }

    public int wDamage { get; set; }

    public int HP { get;set;}
    public int MaxHP { get; set; }

    public bool IsGrounded {  get; set; }

    public Vector3 Movement {  get; set; }
    public PlayerModel
        ( 
        float moveForce,
        float jumpForce, 
        float dashForce, 
        float InterationScale, 
        int wDamage,
        int HP,
        bool IsGrounded,
        Vector3 Movement
        )
    {
        this.MoveForce = moveForce;
        this.JumpForce = jumpForce;
        this.DashForce = dashForce;
        this.InterationScale = InterationScale;
        this.wDamage = wDamage;
        this.HP = HP;
        this.MaxHP = HP;
        this.IsGrounded = IsGrounded;
        this.Movement = Movement;
    }
}
