using UnityEngine;

public enum PlayerCool
{
    Attack,
    Dash,
    dashAttack
}

public class PlayerCooldown
{
    private Cooldown atkCool;
    private Cooldown dashcool;
    private Cooldown dashAttack;

    public PlayerCooldown ()
    {
        atkCool = new Cooldown(1f);
        dashcool = new Cooldown(1f);
        dashAttack = new Cooldown(5f);
    }

    public void Start(PlayerCool cool)
    {
        switch(cool)
        {
            case PlayerCool.Attack:
                atkCool.Start();
                break;
            case PlayerCool.Dash:
                dashcool.Start();
                break;
            case PlayerCool.dashAttack:
                dashAttack.Start();
                break;
        }
    }

    public void TIck(float time)
    {
        atkCool.Tick(time);
        dashcool.Tick(time);
        dashAttack.Tick(time);
    }

    public void Reset(PlayerCool cool)
    {
        switch (cool)
        {
            case PlayerCool.Attack:
                atkCool.Reset();
                break;
            case PlayerCool.Dash:
                dashcool.Reset();
                break;
            case PlayerCool.dashAttack:
                dashAttack.Reset();
                break;
        }
    }

    public bool IsReady(PlayerCool cool)
    {
        bool ready;
        switch (cool)
        {
            case PlayerCool.Attack:
                ready = atkCool.IsReady;
                break;
            case PlayerCool.Dash:
                ready = dashcool.IsReady;
                break;
            case PlayerCool.dashAttack:
                ready = dashAttack.IsReady;
                break;
            default:
                ready = false; 
                break;
        }
        return ready;
    }
}
