using UnityEngine;

public class Door : MonoBehaviour, IInterectable
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        animator.SetTrigger("door_open");

        //Invoke("DoorClose", 10f);
    }

    void DoorClose()
    {
        animator.SetTrigger("door_close");
    }
    
}
