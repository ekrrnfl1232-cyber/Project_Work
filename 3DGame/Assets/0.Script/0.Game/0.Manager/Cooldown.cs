using UnityEngine;

public class Cooldown
{
    private float cooldownTime;
    private float timer;

    public bool IsReady
    {
        get { return timer <= 0f; }
    }

    public Cooldown(float cooldwonTime)
    {
        this.cooldownTime = cooldwonTime;
        timer = 0f;
    }

    public void Start()
    {
        timer = cooldownTime;
    }

    public void Tick(float time)
    {
        if (timer > 0f)
            timer -= time;
    }
}
