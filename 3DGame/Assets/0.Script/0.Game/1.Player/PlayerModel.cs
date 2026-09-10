using UnityEngine;

[System.Serializable]
public class PlayerModel
{
    public float InterationScale { get; set; }

    public Vector3 Movement {  get; set; }

    private float verticalVelocity = 0f;
    public float VerticalVelo { get { return verticalVelocity; } set { verticalVelocity = value; } }
    public Vector3 gravity { get; set; } = Vector3.zero;
    public PlayerModel
        ( 
        PlayerData data
        )
    {
        this.InterationScale = data.InterationScale;
    }
}
