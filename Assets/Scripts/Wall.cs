using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject box;

    private Animator anim;
    private bool hasPlayedAnimation = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Är det spelaren som triggat? Om vi inte har spelat animationen spelas animationen. 
        //Gör så att animationen bara spelas en gång.
        if(other.CompareTag("Player") && !hasPlayedAnimation)
        {
            box.SetActive(false);
            hasPlayedAnimation = true;
            anim.SetTrigger("Move");
        }
    }
}
