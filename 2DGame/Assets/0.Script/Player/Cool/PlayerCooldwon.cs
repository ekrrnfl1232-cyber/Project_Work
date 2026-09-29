using Unity.VisualScripting;
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

    public PlayerCooldown()
    {
        atkCool = new Cooldown(1f);
        dashcool = new Cooldown(1f);
    }

    public void Start(PlayerCool cool)
    {
        switch (cool)
        {
            case PlayerCool.Attack:
                atkCool.Start();
                break;
            case PlayerCool.Dash:
                dashcool.Start();
                break;
        }
    }

    public void TIck(float time)
    {
        atkCool.Tick(time);
        dashcool.Tick(time);
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
        }
    }

    public bool IsReady(PlayerCool cool)
    {
        switch (cool)
        {
            case PlayerCool.Attack:
                return atkCool.IsReady;
            case PlayerCool.Dash:
                return dashcool.IsReady;
            default:
                return false;
        }
    }
}