using UnityEngine;

[System.Serializable]
public class MonsterDeadState : IState
{
    Monster monster;
    public MonsterDeadState (Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        monster.agent.ResetPath();

        monster.IsLive = false;

        monster.MonsterAni.SetFloat("AnimSpeed", 1f);
        monster.MonsterAni.SetTrigger("Dead");
        monster.MonsterAni.SetBool("IsDead", true);
        Debug.Log($"{monster.name} Dead");
        monster.Invoke("OnDead", 2f);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }

}
