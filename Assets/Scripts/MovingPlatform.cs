using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform target1, target2;
    [SerializeField] private float moveSpeed = 2.0f;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = target1;
    }

    
    void FixedUpdate()
    {
        if (transform.position == target1.position)
        {
            currentTarget = target2;
        }

        if (transform.position == target2.position)
        {
            currentTarget = target1;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
    }

    //N�r spelare kolliderar med plattformen blir spelaren child till transform (plattformen)
    //Detta �r f�r att spelaren ska f�lja med plattformen n�r den �ker.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Om spelaren �r �ver plattformen g�r vi spelaren till child, annars inte. 
        if (other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {
            other.transform.SetParent(transform);
        }
    }

    //N�r spelaren l�mnar plattformen ska den inte vara child l�ngre.
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
