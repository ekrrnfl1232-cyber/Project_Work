using UnityEngine;

public enum PlayerCool
{
    Attack,
    Dash
}

public class PlayerCooldown
{
    private Cooldown atkCool;
    private Cooldown dashcool;

    public PlayerCooldown (float atkTime, float dashTime)
    {
        atkCool = new Cooldown(atkTime);
        dashcool = new Cooldown(dashTime);
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
    }

    public void TIck(float time)
    {
        atkCool.Tick(time);
        dashcool.Tick(time);
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
        else
            return false;
    }
}
