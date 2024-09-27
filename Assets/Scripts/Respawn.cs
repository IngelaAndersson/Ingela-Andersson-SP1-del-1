using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    void Update()
    {
        Bossrespawn();
    }
    public void Bossrespawn()
    {
        if (playerMovement.currentHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
