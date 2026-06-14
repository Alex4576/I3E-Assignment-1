using UnityEngine;
using System.Collections;

public class DamageOverTime : MonoBehaviour
{
    public int damagePerSecond = 0; // The amount of damage to apply to the player each second while they are in this area, editable from the Unity Inspector
    private Coroutine damageRoutine;

    private void OnTriggerEnter(Collider other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null && damageRoutine == null) // Check if the player has entered the trigger and we are not already applying damage
        {
            damageRoutine = StartCoroutine(ApplyDamage(player)); // Start the coroutine to apply damage over time to the player
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null && damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }
    }
private IEnumerator ApplyDamage(PlayerScript player)
    {
        while (true) // Loop indefinitely until the player exits the trigger area
        {
            player.TakeDamage(damagePerSecond); // Apply damage to the player each second
            yield return new WaitForSeconds(1f); // Wait for 1 second before applying damage again
        }
    }
}