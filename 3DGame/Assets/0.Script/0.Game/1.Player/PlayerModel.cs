using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private PlayerView view;

    public float InterationScale { get; set; }

    public int HP { get;set;}
    public int MaxHP { get; set; }

    public bool IsGrounded {  get; set; }

    public Vector3 Movement {  get; set; }
    public PlayerModel
        ( 
        float InterationScale,
        int HP,
        Vector3 Movement
        )
    {
        this.InterationScale = InterationScale;
        this.HP = HP;
        this.MaxHP = HP;
        this.Movement = Movement;
    }
}
