using UnityEngine;

[System.Serializable]
public class MonsterReviveState : IState
{
    private Monster monster;

    public MonsterReviveState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        monster.IsLive = true;
        monster.MonsterAni.SetTrigger("Idle");

        monster.Model.HP = monster.data.Hp;

        ObjectPoolManager.Instance.ReturnObject(PoolType.Enemy, monster.gameObject);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
