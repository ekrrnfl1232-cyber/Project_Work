using UnityEngine;

public class MonsterDeadState : IState
{
    Monster monster;
    public MonsterDeadState (Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        monster.isLive = false;
        monster.MonAni.SetTrigger("Dead");
        Debug.Log($"{monster.name} Dead");
        monster.OnDead();
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
