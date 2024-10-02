#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
#endif
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float bounciness = 100;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private AudioClip enemyHit;
    private SpriteRenderer rend;
    private bool canMove = true;
    EnemyCounter enemyCounterScript;

    private AudioSource audioSource;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        // Ingen EnemyCounter beh�vs i level 1. 
        if (SceneManager.GetActiveScene().name == "Level1") 
        {
            enemyCounterScript = null; 
        }
        else
        {
            GameObject enemyCounterObject = GameObject.Find("EnemyCounter");
            if (enemyCounterObject != null)
            {
                enemyCounterScript = enemyCounterObject.GetComponent<EnemyCounter>();
                if (enemyCounterScript == null)
                {
                    Debug.LogError("EnemyCounter-komponenten saknas p� EnemyCounter-objektet");
                }
            }
            else
            {
                Debug.LogError("EnemyCounter-objektet hittades inte i scenen");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed > 0)
        {
            rend.flipX = true;
        }

        if (moveSpeed < 0)
        {
            rend.flipX = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //V�nder riktning om kolliderar med n�got.
        if (other.gameObject.CompareTag("EnemyBlock"))
        {

            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);

            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
            }
        }
    }

    //Om den triggar n�got g�r den hit. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounciness));
            audioSource.PlayOneShot(enemyHit, 0.5f);
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;

            if (enemyCounterScript != null)
            {
                enemyCounterScript.AddKill(); // NYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY
            }
            Destroy(gameObject, 0.4f);
        }
    }

}