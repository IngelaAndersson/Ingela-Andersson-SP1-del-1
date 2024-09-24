using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnterBossFight : MonoBehaviour
{
    public float delay = 0.2f;
    public GameObject boss;
    public GameObject bossHealthBar;
    public CameraBossFight cameraController;
    public Animator gateAnim;
    public Animator exitGateAnim;
    private bool hasPlayedAnimation = false;
   
    [SerializeField] private float delayTime = 2.0f;
    [SerializeField] private GameObject musicPlayer;
    [SerializeField] private AudioSource audioSource;
 
    public string playerTag = "Player";

    private void Start()
    {
        audioSource.Stop();
        gateAnim = transform.Find("Gate").GetComponent<Animator>();
        exitGateAnim = transform.Find("ExitGate").GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag(playerTag) && !hasPlayedAnimation)
        {
            hasPlayedAnimation = true;
            gateAnim.SetTrigger("CloseGate");
            exitGateAnim.SetTrigger("CloseExit");

            musicPlayer.SetActive(false);
            PlayAudioWithDelay();

            Invoke("ActivateBoss", delay);

            if (bossHealthBar != null)
            {
                bossHealthBar.SetActive(true);
            }

            cameraController.StartBossCameraTransition();
        }
    }

    public void PlayAudioWithDelay()
    {
        audioSource.PlayDelayed(delayTime);
    }
    private void ActivateBoss()
    {
        boss.SetActive(true);
        gameObject.SetActive(false);
    }
}
