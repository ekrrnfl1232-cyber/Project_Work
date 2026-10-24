using UnityEngine;

public class EnemyModel
{
    private int hp;
    private float moveSpeed;
    private float attackDamage;
    
    public int HP { get { return hp; } set { hp = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
    public Vector2 MoveDirection { get; set; }
    public Vector3 Movement {  get; set; }

    public EnemyModel()
    {
        hp = 100;
        moveSpeed = 2.0f;
        attackDamage = 10.0f;
        MoveDirection = Vector2.zero;
        Movement = Vector3.zero;
    }
}
