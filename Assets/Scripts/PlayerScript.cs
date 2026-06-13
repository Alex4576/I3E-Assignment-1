using System;
using UnityEngine;
using TMPro;

public class PlayerScript: MonoBehaviour
{
    int flashlightScore = 0; // Store the player's score from collecting flashlights, initialized to 0 at the start of the game
    int documentScore = 0; // Store the player's score from collecting documents, initialized to 0 at the start of the game
    int keyScore = 0; // Store the player's score from collecting keys, initialized to 0 at the start of the game
    int computerScore = 0; // Store the player's score from collecting computers, initialized to 0 at the start of the game
    int thumbdriveScore = 0; // Store the player's score from collecting thumb drives, initialized to 0 at the start of the game

    [SerializeField]
    TextMeshProUGUI scoreText; // Reference to the UI text element that displays the player's score, assigned from the Unity Inspector

    [SerializeField]
    float interactDistance = 3f; // Maximum distance at which the player can interact with objects, editable from the Unity Inspector

    Camera playerCamera; // Reference to the player's camera, used for raycasting to detect interactable objects

    void Start()
    {
        UpdateScoreUI(); // Initialize the score display at the start of the game
        playerCamera = Camera.main; // Get the main camera in the scene, which is assumed to be the player's camera
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    void OnInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            // Flashlight collectible
            if (hit.collider.CompareTag("Flashlight"))
            {
            Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
            if (collectible != null)
            {
                flashlightScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Flashlight score: " + flashlightScore); // Log the player's current flashlight score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
            }
            }
             // Document collectible
            if (hit.collider.CompareTag("Document"))
            {
                Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
                if (collectible != null)
                {
                documentScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Document score: " + documentScore); // Log the player's current document score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
                }
            }
            // Key collectible
            if (hit.collider.CompareTag("Key"))
            {
                Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
                if (collectible != null)
                {
                keyScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Key score: " + keyScore); // Log the player's current key score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
                }
            }
            // Computer collectible
            if (hit.collider.CompareTag("Computer"))
            {
                Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
                if (collectible != null)
                {
                computerScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Computer score: " + computerScore); // Log the player's current computer score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
                }
            }
            // Thumbdrive collectible
            if (hit.collider.CompareTag("Thumbdrive"))
            {
                Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
                if (collectible != null)
                {
                thumbdriveScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Thumbdrive score: " + thumbdriveScore); // Log the player's current thumbdrive score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
                }
            }
            // Door
            DoorScript door = hit.collider.GetComponentInParent<DoorScript>();
            if (door != null)
            {
                // Door1 unlocks if player has collected a flashlight
                if (hit.collider.CompareTag("Door1") && flashlightScore >= 1)
                {
                    door.Interact(); // Call the Interact method on the door to handle opening or closing it
                    Debug.Log("Door1 is unlocked. You can now enter."); // Log a message if the player interacts with Door1 and has the required flashlight
                }
                else if (hit.collider.CompareTag("Door2") && keyScore >= 1)
                {
                    door.Interact(); // Call the Interact method on the door to handle opening or closing it
                    Debug.Log("Door2 is unlocked. You can now enter."); // Log a message if the player interacts with Door2 and has the required key
                }
                else
                {
                    Debug.Log("Door is locked. Search the area for the required item."); // Log a message if the player tries to interact with a door without having collected the required item
                }
                return; // Exit the method after interacting with a door to prevent multiple interactions in one frame
            }
            // Check if we hit the goal area
            int totalScore = flashlightScore + documentScore + keyScore + computerScore + thumbdriveScore; // Calculate the player's total score by summing all score types
            if (hit.collider.CompareTag("GoalArea"))
            {
                if (flashlightScore >= 1 && documentScore >= 1 && keyScore >= 1 && computerScore >= 1 && thumbdriveScore >= 1)
                {
                    Debug.Log("Player Successfully Retrieved all Items!" + totalScore + " points"); // Log a winning message if the player reaches the goal area with enough points
                }
                else
                {
                    Debug.Log("Player have not Retrieved all Items. Current score: " + totalScore); // Log a message if the player reaches the goal area but does not have enough points
                }
            }
        }
        else
        {
            Debug.Log("No interactable object within range"); // Log a message if the player tries to interact but there is no object within the specified distance
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Flashlight: " + flashlightScore + "\nDocument: " + documentScore + "\nKey: " + keyScore + "\nComputer: " + computerScore + "\nThumbdrive: " + thumbdriveScore; // Update the score display to show all score types
    }
}