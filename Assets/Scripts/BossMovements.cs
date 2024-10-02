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

        //if (boss.isSecondStage)
        //{
        //    // Ensure movement is dependent on speed direction (flipped after collision)
        //    transform.Translate(new Vector2(speed, 0) * Time.deltaTime);

        //    // Optionally flip the sprite based on direction
        //    //if (speed > 0)
        //    //{
        //    //    rend.flipX = true;  // Flip sprite to face right
        //    //}
        //    //else if (speed < 0)
        //    //{
        //    //    rend.flipX = false;  // Flip sprite to face left
        //    //}
        //}
    }

    public void SetInvincible(bool state)
    {
        if (state)
        {
            rb.velocity = Vector2.zero;
        }
    }

    //private void MoveBoss()
    //{
    //    if (!boss.isSecondStage)
    //    {
    //        boss.LookAtPlayer();
    //        secondStageMovement = false;
    //        target = new Vector2(player.position.x, rb.position.y);
    //        newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

    //        rb.MovePosition(newPos);
    //    }
    //    else
    //    {
    //        secondStageMovement = true;
    //        speed = 8;
    //        if (moveToCollider)
    //        {
    //            target = new Vector2(collider1.transform.position.x, rb.position.y);
    //            newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
    //            rb.MovePosition(newPos);
    //            if (transform.position.x > 0)
    //            {
    //                rend.flipX = false;
    //            }
    //        }
    //        if (!moveToCollider)
    //        {

    //            target = new Vector2(collider2.transform.position.x, rb.position.y);
    //            newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
    //            rb.MovePosition(newPos);
    //            if (transform.position.x < 0)
    //            {
    //                rend.flipX = true;
    //            }
    //        }
    //    }
    //}
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

            //    // Check for flipping at specified positions
            //    if (transform.position.x >= 16.9f)
            //    {
            //        rend.flipX = false; // Facing right
            //    }
            //    else if (transform.position.x <= -6.59f)
            //    {
            //        rend.flipX = true; // Facing left
            //   }
            }
            else
            {
                target = new Vector2(collider2.transform.position.x, rb.position.y);
                newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

            //    // Check for flipping at specified positions
            //    if (transform.position.x >= 16.9f)
            //    {
            //        rend.flipX = false; // Facing right
            //    }
            //    else if (transform.position.x <= -6.59f)
            //    {
            //        rend.flipX = true; // Facing left
            //    }
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collision is with the "EnemyBlock" and it's the second stage
        if (secondStageMovement && collision.gameObject == collider1)
        {
            //boss.LookAtPlayer();
            //Debug.Log(collision);
            //target = new Vector2(collider2.transform.position.x, rb.position.y);
            //newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            moveToCollider = false;
            //if (transform.position.x < 0)
            //{
            //    rend.flipX = true;
            //}
        }
        if (secondStageMovement && collision.gameObject == collider2)
        {
            //boss.LookAtPlayer();
            //Debug.Log(collision);
            //target = new Vector2(collider1.transform.position.x, rb.position.y);
            //newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            moveToCollider = true;
            //if (transform.position.x > 0)
            //{
            //    rend.flipX = false;
            //}
        }
    }
}




