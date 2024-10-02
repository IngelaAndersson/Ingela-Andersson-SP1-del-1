using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnterBossFight : MonoBehaviour
{
    public float delay = 0.2f;
    public GameObject boss;
    public GameObject trampoline;
    public GameObject bossHealthBar;
    public CameraBossFight cameraController;
    public Animator gateAnim;
    public Animator exitGateAnim;
    private bool hasPlayedAnimation = false;

    [SerializeField] private Gates firstGate;
    [SerializeField] private Gates secondGate;
    [SerializeField] private Gates audioClip;
    [SerializeField] private float delayTime = 2.0f;
    [SerializeField] private GameObject musicPlayer;
    [SerializeField] private AudioSource audioSource;

    public string playerTag = "Player";

    private void Start()
    {
        audioSource.Stop();
        gateAnim = GetComponent<Animator>();
        exitGateAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag(playerTag) && !hasPlayedAnimation)
        {
            hasPlayedAnimation = true;
   
            firstGate.CloseGate();
            secondGate.CloseGate();

            audioClip.PlayGateSound();
           
            PlayAudioWithDelay();

            musicPlayer.SetActive(false);

            trampoline.SetActive(true);

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
