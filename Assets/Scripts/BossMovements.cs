using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class BossMovements : MonoBehaviour

{
    [SerializeField] Boss boss;
    [SerializeField] GameObject collider1;
    [SerializeField] GameObject collider2;

    public float delayBeforeMove = 3f;
    public float speed = 5f;
    private bool canMove = false;
    private float elapsedTime = 0f;

    [SerializeField] private SpriteRenderer rend;
    private Transform player;
    private Rigidbody2D rb;
    private Boss bossScript;
    private bool secondStageMovement;
    private bool moveToCollider;
    private Vector2 target;
    private Vector2 newPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        bossScript = GetComponent<Boss>();

        elapsedTime = 0f;
        canMove = false;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= delayBeforeMove)
        {
            canMove = true;
        }

        if (canMove & (!boss.IsInvincible()))
        {
            MoveBoss();
        }
    }

    public void SetInvincible(bool state)
    {
        if (state)
        {
            rb.velocity = Vector2.zero;
        }
    }

  
    public void MoveBoss()
    {
        if (!boss.isSecondStage)
        {
            boss.LookAtPlayer();
            secondStageMovement = false;
            target = new Vector2(player.position.x, rb.position.y);
            newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

            rb.MovePosition(newPos);
        }
        else
        {
            secondStageMovement = true;
            speed = 8;
            boss.LookAtPlayer();
            

            if (moveToCollider)
            {
                target = new Vector2(collider1.transform.position.x, rb.position.y);
                newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

         
            }
            else
            {
                target = new Vector2(collider2.transform.position.x, rb.position.y);
                newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

          
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (secondStageMovement && collision.gameObject == collider1)
        {
          
            moveToCollider = false;
          
        }
        if (secondStageMovement && collision.gameObject == collider2)
        {
          
            moveToCollider = true;
          
        }
    }
}




