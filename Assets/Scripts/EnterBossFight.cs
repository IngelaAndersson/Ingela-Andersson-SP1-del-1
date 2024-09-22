using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnterBossFight : MonoBehaviour
{
    public GameObject boss;
    public float delay = 0.2f;
    public GameObject bossHealthBar;
    public CameraBossFight cameraController;

    [SerializeField] private GameObject musicPlayer;
    [SerializeField] private AudioSource audioSource;
 

    // Reference to the player tag or GameObject
    public string playerTag = "Player";

    private void Start()
    {
        audioSource.Stop();
    }
    // Method that detects when something enters the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing entering the trigger is the player
        if (other.CompareTag(playerTag))
        {
            musicPlayer.SetActive(false);
            audioSource.Play();
            Invoke("ActivateBoss", delay);

            if (bossHealthBar != null)
            {
                bossHealthBar.SetActive(true);
            }

            // Start camera transition
            cameraController.StartBossCameraTransition();
        }
    }
     private void ActivateBoss()
    {
        boss.SetActive(true);

        // Optionally, disable the trigger to prevent retriggering
        gameObject.SetActive(false);
    }
}
