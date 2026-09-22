using UnityEngine;

public class PlayerAreaSkillState : IState
{
    private Player player;
    private GameObject range;

    public PlayerAreaSkillState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        range = player.view.area;
        range.SetActive(true);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            range.transform.position = hit.point;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Collider[] targetCheck = Physics.OverlapSphere
            (range.transform.position, 4f, LayerMask.GetMask("Monster"));
            foreach(var tar in targetCheck)
            {
                if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
                {
                    damage.TakeDamage(player.stat.AreaDamage);
                    break;
                }
            }
            range.SetActive(false);
            player.IsTargeting = false;
            player.Cool.Start(PlayerCool.Area);
            player.ChangeState(PlayerState.idleState);
        }
    }
}
