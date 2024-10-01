using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{

    [SerializeField] Animator gateAnim;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private AudioClip gateSound;



    void Start()
    {
        gateAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenGate()
    {
        gateAnim.SetTrigger("OpenGate");
    }
    public void CloseGate()
    {
        gateAnim.SetTrigger("CloseGate");

    }
    public void PlayGateSound()
    {
        audioSource.PlayOneShot(gateSound, 0.3f);
    }

}
