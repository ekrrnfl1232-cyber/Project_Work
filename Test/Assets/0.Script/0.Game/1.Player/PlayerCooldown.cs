using UnityEngine;

public enum PlayerCool
{
    Attack,
    Dash,
    Area
}

public class PlayerCooldown
{
    private Cooldown atkCool;
    private Cooldown dashcool;
    private Cooldown area;

    public PlayerCooldown ()
    {
        atkCool = new Cooldown(1f);
        dashcool = new Cooldown(1f);
        area = new Cooldown(2f);
    }

    public void Start(PlayerCool cool)
    {
        if (cool == PlayerCool.Attack)
        {
            atkCool.Start();
        }
        if (cool == PlayerCool.Dash)
        {
            dashcool.Start();
        }
        if(cool == PlayerCool.Area)
        {
            area.Start();
        }
    }

    public void TIck(float time)
    {
        atkCool.Tick(time);
        dashcool.Tick(time);
        area.Tick(time);
    }

    public void Reset(PlayerCool cool)
    {
        if (cool == PlayerCool.Attack)
        {
            atkCool.Reset();
        }
        if (cool == PlayerCool.Dash)
        {
            dashcool.Reset();
        }
        if (cool == PlayerCool.Area)
        {
            area.Reset();
        }
    }

    public bool IsReady(PlayerCool cool)
    {
        if(cool == PlayerCool.Attack)
        {
            return atkCool.IsReady;
        }
        if(cool == PlayerCool.Dash)
        {
            return dashcool.IsReady;
        }
        if(cool == PlayerCool.Area)
        {
            return area.IsReady;
        }
        return false;
    }
}
