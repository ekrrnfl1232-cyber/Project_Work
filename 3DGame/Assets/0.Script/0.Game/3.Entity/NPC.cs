using UnityEngine;

public class NPC : MonoBehaviour, IInterectable
{
    public void Interact()
    {
        Debug.Log("대화 상호작용");
    }
}
