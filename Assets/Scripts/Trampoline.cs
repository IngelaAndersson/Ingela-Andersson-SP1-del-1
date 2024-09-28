using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private AudioClip trampolineSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Om det �r spelaren som hoppar p� trampolinen sparas spelarens rigidbody i denna.
        if(other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            //Varje g�ng vi tr�ffar trampolinen nollst�ller vi hastigheten
            //Detta f�r att kraften n�r vi faller inte ska motverka uppstudsen. 
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(new Vector2 (0, jumpForce));
            //G�r till animationen f�r trampolinen och spelar den.
            GetComponent<Animator>().SetTrigger("Jump");
            audioSource.PlayOneShot(trampolineSound, 0.5f);

        }
    }
}
