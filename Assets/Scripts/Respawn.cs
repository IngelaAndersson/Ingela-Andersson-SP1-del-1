using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Animator playerAnimator;
    private void Start()
    {
        //Gameobject.setactive(false);
    }
    void Update()
    {
        Bossrespawn();
    }
    public void Bossrespawn()
    {
        if (playerMovement.currentHealth <= 0)
        {
            //gameobject setactive = true
            playerMovement.enabled = false;
            playerAnimator.SetTrigger("Die");
            Invoke("LoadSceneRespawn", 1f);
        }
    }

    private void LoadSceneRespawn()
    {
        SceneManager.LoadScene(3);
    }

    
}
