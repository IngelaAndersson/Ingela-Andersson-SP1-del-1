using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private float bounciness = 100;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] float health, maxHealth = 10f;
    [SerializeField] private Slider healthSlider;


    public Transform player;
    public bool isFlipped = false;

    private void Start()
    {
        health = maxHealth;
    }


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        UpdateHealthBar();

        if (health <= 0)
        {
            GetComponent<Animator>().SetTrigger("Defeated");

            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = health;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            if (other.gameObject == GameObject.FindWithTag("Player"))
                TakeDamage(1);

            if (other.CompareTag("Player"))
            {
                GetComponent<Animator>().SetTrigger("Hit");
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounciness));
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            }
        }

    }
}
