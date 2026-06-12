using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator myAnimator;

    bool isOpen = false;

    void Start()
    {
        myAnimator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        if(myAnimator != null)
        {
            if(!isOpen)
            // Play the open or close animation
                myAnimator.SetTrigger("DoorOpen");
            else
                myAnimator.SetTrigger("DoorClose");

            isOpen = !isOpen; // Toggle the door state for the next interaction
        }
    }
}
