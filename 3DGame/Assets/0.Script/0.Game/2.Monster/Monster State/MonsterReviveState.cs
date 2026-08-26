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

        monster.MonsterAni.SetBool("IsDead", false);
        monster.MonsterAni.SetFloat("AnimSpeed", -1f);
        monster.MonsterAni.Play("Mini Simple Characters Armature|Loose", 0, 1f);

        monster.Model.HP = monster.data.Hp;
        monster.ChangeState("idleState");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
