/*
*Author: Alex
*Date: 14/6/2026
*Description: This script defines the behavior of doors in the game. It handles the opening and closing animations when the player interacts with the door.
*/

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
       if (myAnimator != null && !isOpen)
        {
            myAnimator.SetTrigger("DoorOpen"); // Trigger the "DoorOpen" animation on the animator to play the door opening animation when the player interacts with it
            isOpen = true; // Set the isOpen flag to true to indicate that the door has been opened, preventing it from being opened again
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen && other.CompareTag("Player"))
        {
            if (myAnimator != null)
            {
                myAnimator.SetTrigger("DoorClose"); // Trigger the "DoorClose" animation on the animator to play the door closing animation when the player exits the trigger area of the door
                isOpen = false; // Set the isOpen flag back to false to allow the door to be opened again in the future
            }
    }
    }
}
