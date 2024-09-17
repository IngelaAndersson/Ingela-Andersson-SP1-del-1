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

    //När spelare kolliderar med plattformen blir spelaren child till transform (plattformen)
    //Detta är för att spelaren ska följa med plattformen när den åker.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Om spelaren är över plattformen gör vi spelaren till child, annars inte. 
        if (other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {
            other.transform.SetParent(transform);
        }
    }

    //När spelaren lämnar plattformen ska den inte vara child längre.
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
