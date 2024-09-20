using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnterBossFight : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHealthBar;
    public CameraBossFight cameraController;

    // Reference to the player tag or GameObject
    public string playerTag = "Player";

    // Method that detects when something enters the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing entering the trigger is the player
        if (other.CompareTag(playerTag))
        {
            // Activate the boss
            boss.SetActive(true);

            if (bossHealthBar != null)
            {
                bossHealthBar.SetActive(true);
            }
            // Start camera transition
            cameraController.StartBossCameraTransition();

            // Optional: Disable the trigger after the boss is activated
            gameObject.SetActive(false);
        }
    }

}
