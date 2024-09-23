using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovement : StateMachineBehaviour
{

    public float delayBeforeMove = 3f;
    public float speed = 5f;
    private bool canMove = false;
    private float elapsedTime = 0f; 
    private Boss bossScript;

    Transform player;
    Rigidbody2D rb;
    [SerializeField] Boss boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        elapsedTime = 0f;
        canMove = false;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


      
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= delayBeforeMove)
        {
  
            canMove = true;
        }

        if (canMove)
        {
            MoveBoss(animator);
        }

        {
            if (bossScript != null)
            {
             
                bossScript.transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

        }
    }
        

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        canMove = false;
    }

    private void MoveBoss(Animator animator)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
}
