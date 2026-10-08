using UnityEngine;

public class PlayerLook
{
    private Player player;
    Vector3 mousePos;

    public PlayerLook(Player player)
    {
        this.player = player;
        mousePos = new Vector3();
    }

    public void Tick(Vector3 pos)
    {
        mousePos = pos;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane aimPlane = new Plane(Vector3.up, player.transform.position);
        if (!aimPlane.Raycast(ray, out float distance))
            return;
        Vector3 aimPoint = ray.GetPoint(distance);
        Vector3 dir = aimPoint - player.transform.position;
        dir.y = 0f;

        if(dir.sqrMagnitude > 0.001f)
        {
            player.transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
