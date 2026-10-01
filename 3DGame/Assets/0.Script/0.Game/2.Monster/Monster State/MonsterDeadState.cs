using UnityEngine;

[System.Serializable]
public class MonsterDeadState : IState
{
    Monster monster;
    float time;
    public MonsterDeadState (Monster monster)
    {
        this.monster = monster;
        time = 2f;
    }

    public void Enter()
    {
        monster.agent.ResetPath();
        monster.MonsterAni.SetFloat("AnimSpeed", 1f);
        monster.MonsterAni.SetTrigger("Dead");
        monster.MonsterAni.SetBool("IsDead", true);
        monster.View.HpUpdate(monster.Model.HP, monster.Model.MaxHP);
        monster.Invoke("OnDead", 2f);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        time -= Time.deltaTime;
    }

}
