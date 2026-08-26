using UnityEngine;

[System.Serializable]
public class MonsterHitState : IState
{
    Monster monster;
    public MonsterHitState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        monster.Model.HP -= monster.Model.Damage;
        if (monster.Model.HP <= 0)
        {
            monster.ChangeState("deadState");
        }
        else
        {
            monster.MonsterAni.SetTrigger("Hit");
            Debug.Log($"남은 체력 : {monster.Model.HP}");
            monster.ChangeState(monster.PrevState);
        }
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        
    }
}
