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
        monster.model.HP -= damage;
        monster.view.HpUpdate(monster.model.HP, monster.model.MaxHP);
        if (monster.model.HP <= 0)
        {
            Debug.Log($"{monster.name} Dead");
            monster.gameObject.SetActive(false);
            monster.view.HPbarDelete(false);
        }
        else
            Debug.Log($"남은 체력 : {monster.model.HP}");
        monster.ChangeState(prevState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
