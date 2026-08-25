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
    private float jumpForce;
    public float JumpForce { get { return jumpForce; } }

    [SerializeField]
    private float dashForce;
    public float DashForce { get { return dashForce; } }

    [SerializeField]
    private float moveForce;
    public float MoveForce { get {return moveForce; } }

    [SerializeField]
    private int damage;
    public int Wdamage { get { return damage; } }
}
