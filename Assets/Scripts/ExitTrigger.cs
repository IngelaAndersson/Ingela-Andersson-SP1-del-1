using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;  

public class ExitTrigger : MonoBehaviour
{
    public Canvas outroCanvas;  
    public float outroDuration = 3f;  
    public float fadeDuration = 1.5f;  
    private bool outroStarted = false;
    public SpriteRenderer playerSprite;

   [SerializeField] PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player") && !outroStarted)
        {
            playerMovement.enabled = false; 
            
            playerSprite = other.GetComponent<SpriteRenderer>();

            StartOutro();  
        }
    }

    void StartOutro()
    {
        outroStarted = true;
        StartCoroutine(FadeOutPlayer()); 
    }

    IEnumerator FadeOutPlayer()
    {
        float elapsedTime = 0f;
        Color originalColor = playerSprite.color;  

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration); 
            playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;  
        }

        playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);  
        ShowOutroCanvas(); 
    }

    void ShowOutroCanvas()
    {
        outroCanvas.gameObject.SetActive(true);  
        Invoke("LoadMenuScene", outroDuration);  
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene(0);  
    }
}
