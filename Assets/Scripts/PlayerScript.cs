using System;
using UnityEngine;
using TMPro;

public class PlayerScript: MonoBehaviour
{
    // Score variables for each type of collectible, initialized to 0 at the start of the game
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

    // Player's Health
    [Header("Player's Health")]
    [SerializeField]
    public int maxHealth = 100; // Player's current health, editable from the Unity Inspector
    private int currentHealth; // Player's current health, initialized to the maximum health

    [SerializeField] TextMeshProUGUI healthText; // Reference to the UI text element that displays the player's health, assigned from the Unity Inspector
    
    // Screens
    [Header("Screens")]
    [SerializeField] GameObject loseScreen; // Reference to the game object that represents the lose screen, assigned from the Unity Inspector
    [SerializeField] GameObject winScreen; // Reference to the game object that represents the win screen, assigned from the Unity Inspector
    [SerializeField] GameObject dotImage; // Reference to the game object that represents the dot image, assigned from the Unity Inspector, used for guide for raycasting    
    void Start()
    {
        UpdateScoreUI(); // Initialize the score display at the start of the game
        playerCamera = Camera.main; // Get the main camera in the scene, which is assumed to be the player's camera

        currentHealth = maxHealth; // Set the player's current health to the maximum health at the start of the game
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + "/" + maxHealth; // Initialize the health display to show the player's current health and maximum health
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    // Damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce the player's current health by the specified damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure the player's health does not go below 0 or above the maximum health
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + "/" + maxHealth; // Update the health display to show the current health and maximum health
        }
        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth); // Log a message indicating how much damage the player took and their current health

        if (currentHealth <= 0)
        {
            Debug.Log("You died."); // Log a message when the player's health reaches 0 or below, indicating that the player has died
            // Game over
            ShowLoseScreen(); // Call a method to show the game over screen (this method would need to be implemented separately)
        }
    }

    void ShowLoseScreen()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
            if (scoreText != null) scoreText.gameObject.SetActive(false); // Hide the score text when the lose screen is shown
            if (healthText != null) healthText.gameObject.SetActive(false); // Hide the health text when the lose screen is shown
            if (dotImage != null) dotImage.SetActive(false); // Hide the dot image when the lose screen is shown
            Time.timeScale = 0f ; // Pause the game when the lose screen is shown
        }
    }

    void ShowWinScreen()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
            if (scoreText != null) scoreText.gameObject.SetActive(false); // Hide the score text when the win screen is shown
            if (healthText != null) healthText.gameObject.SetActive(false); // Hide the health text when the win screen is shown
            if (dotImage != null) dotImage.SetActive(false); // Hide the dot image
            Time.timeScale = 0f ; // Pause the game when the win screen is shown
        }
    }

    void OnInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            // Wooden Crate
            if (hit.collider.CompareTag("Crate"))
            {
                Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
            if (collectible != null)
            {
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
            }
            }

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

            // Check if we hit the goal area as well as collected all required items
            if (hit.collider.CompareTag("GoalArea"))
            {
                if (flashlightScore >= 1 && documentScore >= 6 && keyScore >= 2 && computerScore >= 1 && thumbdriveScore >= 2)
                {
                    Debug.Log("Congratulations! You have collected all required items and exited the building."); // Log a winning message if the player reaches the goal area with enough points
                    ShowWinScreen(); // Call a method to show the win screen (this method would need to be implemented separately)
                }
                else
                {
                    Debug.Log("Please collect all required items before exiting."); // Log a message if the player reaches the goal area but does not have enough points
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
        scoreText.text = "Flashlight: " + flashlightScore + "/1\nDocument: " + documentScore + "/6\nKey: " + keyScore + "/2\nComputer: " + computerScore + "/1\nThumbdrive: " + thumbdriveScore + "/2"; // Update the score display to show all score types
    }

    // Damage over time
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            DamageObject dmg = other.GetComponent<DamageObject>();
            if (dmg != null)
            {
                TakeDamage(dmg.damageAmount); // Call the TakeDamage method with the damage amount from the DamageObject when the player enters a trigger with the "Damage" tag
            }
        }
    }
}