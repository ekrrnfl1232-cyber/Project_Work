using UnityEngine;

public enum PlayerCool
{
    Attack,
    Dash,
    dashAttack,
    fangAttack,
    RootBind
}

public class PlayerCooldown
{
    // 플레이어 기본 쿨
    private Cooldown atkCool;
    private Cooldown dashcool;

    // 플레이어 스킬 쿨
    private Cooldown dashAttack;
    private Cooldown fangAttack;
    private Cooldown rootBind;

    public PlayerCooldown ()
    {
        atkCool = new Cooldown(1f);
        dashcool = new Cooldown(1f);

        fangAttack = new Cooldown(4f);
        dashAttack = new Cooldown(6f);
        rootBind = new Cooldown(10f);
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
            case PlayerCool.fangAttack:
                fangAttack.Start();
                break;
            case PlayerCool.RootBind:
                rootBind.Start();
                break;
        }
    }

    public void TIck(float time)
    {
        atkCool.Tick(time);
        dashcool.Tick(time);
        dashAttack.Tick(time);
        fangAttack.Tick(time);
        rootBind.Tick(time);
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
            case PlayerCool.fangAttack:
                fangAttack.Reset();
                break;
            case PlayerCool.RootBind:
                rootBind.Reset();
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
            case PlayerCool.fangAttack:
                ready = fangAttack.IsReady;
                break;
            case PlayerCool.RootBind:
                ready = rootBind.IsReady;
                break;
            default:
                ready = false; 
                break;
        }
        return ready;
    }

    public float GetTime(PlayerCool cool)
    {
        float time;
        switch (cool)
        {
            case PlayerCool.Attack:
                time = atkCool.RemainTime;
                break;
            case PlayerCool.Dash:
                time = dashcool.RemainTime;
                break;
            case PlayerCool.dashAttack:
                time = dashAttack.RemainTime;
                break;
            case PlayerCool.fangAttack:
                time = fangAttack.RemainTime;
                break;
            case PlayerCool.RootBind:
                time = rootBind.RemainTime;
                break;
            default:
                time = 0f; 
                break;
        }
        return time;
    }
}
