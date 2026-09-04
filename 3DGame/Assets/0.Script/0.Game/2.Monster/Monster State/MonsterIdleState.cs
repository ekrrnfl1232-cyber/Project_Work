using UnityEngine;

[System.Serializable]
public class MonsterIdleState : IState
{
    Monster monster;

    public MonsterIdleState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("¥Î±‚¡ﬂ");
        monster.MonsterAni.SetTrigger("Idle");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (monster.IsFind && monster.Model.TargetDis <= monster.data.Range && monster.IsLive)
        {
            monster.transform.LookAt(monster.target);
            if (monster.attackCool.IsReady)
            {
                monster.ChangeState("attackState");
            }
        }
        if (monster.IsFind && monster.Model.TargetDis > monster.data.Range)
            monster.ChangeState("moveState");
    }
}
