using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MonsterMoveState : IState
{
    Monster monster;
    public MonsterMoveState(Monster monster)
    {
        this.monster = monster;
    }
    public void Enter()
    {
        
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        Vector3 tPos = monster.target.position;
        Vector3 mPos = monster.transform.position;
        mPos.y = 0;
        tPos.y = 0;
        monster.model.TarPos = tPos;
        monster.transform.position = Vector3.MoveTowards(mPos, monster.model.TarPos, 1f * Time.deltaTime);
    }
}
