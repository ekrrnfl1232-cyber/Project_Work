using UnityEngine;

[System.Serializable]
public class PlayerModel
{
    private PlayerView view;

    public float InterationScale { get; set; }

    public int HP { get; set; }

    public bool IsGrounded {  get; set; }

    public float MaxExp { get; set; }

    public Vector3 Movement {  get; set; }

    private float verticalVelocity = 0f;
    public float VerticalVelo { get { return verticalVelocity; } set { verticalVelocity = value; } }
    public Vector3 gravity { get; set; } = Vector3.zero;
    public PlayerModel
        ( 
        float InterationScale,
        int HP,
        Vector3 Movement,
        float maxExp 
        )
    {
        this.InterationScale = InterationScale;
        this.HP = HP;
        this.Movement = Movement;
        MaxExp = maxExp;
    }
}
