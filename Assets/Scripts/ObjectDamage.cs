/*
*Author: Alex
*Date: 14/6/2026
*Description: This script defines the behavior of damage objects in the game. It applies damage to the player when they interact with it.
*/

using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int damageAmount = 0;

    private void OnTriggerEnter(Collider other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            player.TakeDamage(damageAmount); // Call the TakeDamage method on the player to apply damage when they enter the trigger area of this object
        }
    }
}
