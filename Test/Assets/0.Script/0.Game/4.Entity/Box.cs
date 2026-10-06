using UnityEngine;

public class Box : MonoBehaviour, IInterectable
{
    public void Interact()
    {
        Debug.Log("상자 상호작용");
    }
}
