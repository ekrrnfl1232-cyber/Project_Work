using UnityEngine;

public class MonsterHitState : IState
{
    Monster monster;
    IState prevState;
    int damage;
    public MonsterHitState(Monster monster, IState prevState,int damage)
    {
        this.monster = monster;
        this.prevState = prevState;
        this.damage = damage;
    }

    public void Enter()
    {
        monster.Mmodel.HP -= damage;
        monster.view.HpUpdate(monster.Mmodel.HP, monster.Mmodel.MaxHP);
        if (monster.Mmodel.HP <= 0)
        {
            monster.ChangeState(new MonsterDeadState(monster));
        }
        else
        {
            monster.MonAni.SetTrigger("Hit");
            Debug.Log($"남은 체력 : {monster.Mmodel.HP}");
            monster.ChangeState(prevState);
        }
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
