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

    //Om spelaren g�r in i omr�det visas rutan.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //G�r in p� spelarens komponenter, kollar hur m�nga �pplen vi samlat, och kollar om det
            //�r st�rre eller lika mycket som v�rat goal (10)
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

    //Om spelaren g�r ut ur omr�det f�rsvinner rutan.
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
