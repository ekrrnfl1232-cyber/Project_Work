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
        monster.transform.position = monster.Model.StartPos;

        monster.gameObject.SetActive(true);
        monster.View.CreateHp();
        monster.IsLive = true;

        monster.MonsterAni.SetTrigger("Idle");

        monster.Model.HP = monster.data.Hp;
        
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
