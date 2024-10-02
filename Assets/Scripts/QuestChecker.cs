using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 10;
    [SerializeField] private int levelToLoad;
    [SerializeField] private AudioClip levelCompleteSound;
    [SerializeField] private GameObject musicPlayer;

    private int scene;
    private AudioSource audioSource;
    public EnemyCounter enemycount;


    private Animator anim;
    private bool levelIsLoading = false;
    private bool hasPlayedAudio = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    //Om spelaren g�r in i omr�det visas rutan.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasPlayedAudio && other.CompareTag("Player"))
        {
            Debug.Log("Spelaren gick in i triggern!");

            // Kolla om vi �r p� level 1
            if (scene == 1)
            {
                Debug.Log("Vi �r p� level 1, kollar antal �pplen.");

                if (other.GetComponent<PlayerMovement>().applesCollected >= questGoal)
                {
                    Debug.Log("Quest f�r �pplen klart!");

                    dialogueBox.SetActive(true);
                    finishedText.SetActive(true);
                    unfinishedText.SetActive(false);
                    anim.SetTrigger("Flag");
                    audioSource.PlayOneShot(levelCompleteSound, 0.5f);
                    hasPlayedAudio = true;
                    musicPlayer.SetActive(false);
                    Invoke("LoadNextLevel", 6.0f);
                    levelIsLoading = true;
                }
                else
                {
                    Debug.Log("Inte tillr�ckligt m�nga �pplen samlade.");

                    dialogueBox.SetActive(true);
                    unfinishedText.SetActive(true);
                    finishedText.SetActive(false);
                }

            }
            // Om vi �r p� level 2
            else if (scene == 2)  // Kollar specifikt f�r Level 2
            {

                Debug.Log("Vi �r p� level 2, kollar antal kills.");
                Debug.Log("Antal fiender d�dade: " + enemycount.kills);

                if (enemycount.kills >= questGoal)  // Kollar antalet d�dade fiender
                {
                    Debug.Log("Quest f�r fiender klart!");

                    dialogueBox.SetActive(true);
                    finishedText.SetActive(true);  // Visa "quest klart"-texten
                    unfinishedText.SetActive(false);  // D�lja "quest ej klart"-texten
                    anim.SetTrigger("Flag");  // Trigga animation
                    audioSource.PlayOneShot(levelCompleteSound, 0.5f);
                    hasPlayedAudio = true;
                    musicPlayer.SetActive(false);
                    Invoke("LoadNextLevel", 6.0f);  // Ladda n�sta scen efter 4 sekunder
                    levelIsLoading = true;
                }
                else
                {
                    Debug.Log("Inte tillr�ckligt m�nga fiender d�dade.");

                    dialogueBox.SetActive(true);
                    unfinishedText.SetActive(true);  // Visa "quest ej klart"-texten
                    finishedText.SetActive(false);  // D�lja "quest klart"-texten
                }
            }
        }
    }

    private void LoadNextLevel()
    {
        Debug.Log("Laddar n�sta scen: " + levelToLoad);
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
