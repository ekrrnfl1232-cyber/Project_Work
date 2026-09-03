using UnityEngine;

[CreateAssetMenu(fileName = "PD")]
public class PlayerData : ScriptableObject 
{
    [SerializeField]
    private int hp;
    public int HP { get { return hp; } }
    [SerializeField]
    private int maxhp;
    public int Maxhp { get { return maxhp; } }

    [SerializeField]
    private float jumpHeight;
    public float JumpHeight { get { return jumpHeight; } }

    [SerializeField]
    private float dashForce;
    public float DashForce { get { return dashForce; } }

    [SerializeField]
    private float moveForce;
    public float MoveForce { get {return moveForce; } }

    [SerializeField]
    private int damage;
    public int Wdamage { get { return damage; } }

    [SerializeField]
    private float maxExp;
    public float MaxExp { get { return maxExp; } }

}
