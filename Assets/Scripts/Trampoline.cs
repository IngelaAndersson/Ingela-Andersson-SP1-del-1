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
        //Om det är spelaren som hoppar på trampolinen sparas spelarens rigidbody i denna.
        if(other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            //Varje gång vi träffar trampolinen nollställer vi hastigheten
            //Detta för att kraften när vi faller inte ska motverka uppstudsen. 
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(new Vector2 (0, jumpForce));
            //Går till animationen för trampolinen och spelar den.
            GetComponent<Animator>().SetTrigger("Jump");
            audioSource.PlayOneShot(trampolineSound, 0.5f);

        }
    }
}
