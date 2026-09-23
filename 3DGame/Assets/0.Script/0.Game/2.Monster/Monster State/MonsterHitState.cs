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
        monster.View.HpUpdate(monster.Model.HP, monster.Model.MaxHP);
        monster.MonsterAni.SetTrigger("Hit");
        Debug.Log($"남은 체력 : {monster.Model.HP}");
        monster.ChangeState(MonsterState.idle);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        
    }
}
