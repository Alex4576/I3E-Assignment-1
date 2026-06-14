using UnityEngine;

public class Collectible : MonoBehaviour
{

    public Vector3 rotation;
    void Update()
    {
        this.transform.Rotate(rotation * 1 * Time.deltaTime); // Rotate the collectible around its own axis at a speed defined by the rotation variable, creating a visual effect to make it more noticeable and appealing to the player
    }

    public int collectibleScore = 0; // Store the score value of this collectible, editable from the Unity Inspector. (this allows different collectibles to be worth different amounts of points)

    AudioSource collectibleAudio;
    private bool isCollected = false; // Flag to prevent multiple collections of the same item, initialized to false at the start of the game

    void Start()
    {
        collectibleAudio = GetComponent<AudioSource>();
    }

    public void Collect() // Custom method to handle the collection of this item, called from the PlayerScript when the player interacts with it
    {
        if (isCollected) return; // If the item is already collected, exit the method

        isCollected = true; // Mark the item as collected

        if(collectibleAudio != null) // Check if we have an AudioSource component to play a sound
        {
            collectibleAudio.Play(); // Play the collection sound effect for feedback when the item is collected
        }
        else
        {
            print("Warning: No AudioSource found on " + gameObject.name); // Log a warning if there is no audio component, but still allow collection to proceed
        }
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false; // Disable the collider to prevent further interactions with this collectible after it has been collected
        float delay = (collectibleAudio != null && collectibleAudio.clip != null) ? collectibleAudio.clip.length : 0f;
        Destroy(gameObject, delay); // Destroy the collectible object after the sound has finished playing (or immediately if there is no sound)
    }
}
