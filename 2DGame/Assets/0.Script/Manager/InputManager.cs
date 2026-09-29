using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public InputSystem_Actions input { get; private set; }

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
