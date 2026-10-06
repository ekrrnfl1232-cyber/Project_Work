using System;
using UnityEngine;

public class InputManger : Singleton<InputManger>
{
    public InputSystem_Actions input {  get; private set; }

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input?.Enable();
    }
    private void OnDisable()
    {
        input?.Disable();
    }
}
