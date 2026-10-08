using UnityEngine;

public class PlayerInteract
{
    private Player player;
    private IInterectable target;
    private bool isFind;

    public PlayerInteract (Player player)
    {
        this.player = player;
        isFind = false;
        target = null;
    }

    public void Tick()
    {
        Vector3 posInteract = player.transform.position;
        posInteract.y += 1f;
        Collider[] colls = Physics.OverlapSphere(posInteract, player.stat.InterationScale);
        isFind = false;
        target = null;
        foreach (var col in colls)
        {
            if(col.TryGetComponent<IInterectable>(out IInterectable interact))
            {
                isFind = true;
                target = interact;
                break;
            }
        }

        player.view.CheckBox(isFind);
    }

    public void Interact()
    {
        Debug.Log("inter");
        target?.Interact();
    }
}
