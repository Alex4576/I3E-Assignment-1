using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public int collectibleScore = 0; // Store the score value of this collectible, editable from the Unity Inspector. (this allows different collectibles to be worth different amounts of points)

    AudioSource collectibleAudio;

    void Start()
    {
        collectibleAudio = GetComponent<AudioSource>();
    }

    public void Collect() // Custom method to handle the collection of this item, called from the PlayerScript when the player interacts with it
    {
        if(collectibleAudio != null) // Check if we have an AudioSource component to play a sound
        {
            collectibleAudio.Play(); // Play the collection sound effect for feedback when the item is collected
        }
        else
        {
            print("Warning: No AudioSource found on " + gameObject.name); // Log a warning if there is no audio component, but still allow collection to proceed
        }
        Destroy(gameObject, collectibleAudio != null ? collectibleAudio.clip.length : 0f); // Destroy the collectible object after the sound has finished playing (or immediately if there is no sound)
    }
}
