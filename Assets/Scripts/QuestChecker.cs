using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 10;
    [SerializeField] private int levelToLoad;

    private Animator anim;
    private bool levelIsLoading = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Om spelaren går in i området visas rutan.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Går in på spelarens komponenter, kollar hur många äpplen vi samlat, och kollar om det
            //är större eller lika mycket som vårat goal (10)
            if(other.GetComponent<PlayerMovement>().applesCollected >= questGoal)
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
                anim.SetTrigger("Flag");
                Invoke("LoadNextLevel", 4.0f);
                levelIsLoading = true;
            }
            else
            {
                dialogueBox.SetActive(true);
                unfinishedText.SetActive(true);
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);

    }

    //Om spelaren går ut ur området försvinner rutan.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelIsLoading)
        {
            dialogueBox.SetActive(false);
            finishedText.SetActive(false);
            unfinishedText.SetActive(false);
        }
    }
}
