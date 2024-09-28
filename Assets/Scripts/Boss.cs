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
    [SerializeField] private AudioSource victoryAudio;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private Gate firstGate;
    [SerializeField] private Gate secondGate;

    public float normalSpeed = 2.0f;
    private float invincibilityTimer = 0f;
    public float invincibilityDuration = 1.0f;
    public Vector3 enlargedScale = new Vector3(2f, 2f, 2f);
    private Vector3 initialScale;
    private Animator animator;
    private BossMovements bossMovement;
    public bool isSecondStage = false;
    public Color invincibleColor = Color.red;

    public Animator bossAnimator;
    private SpriteRenderer spriteRenderer;
    public Transform player;
    public float pushForce = 5f;

    public bool isFlipped = false;

    private void Start()
    {
        health = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();

        initialScale = transform.localScale;

        bossMovement = GetComponent<BossMovements>();

        animator = GetComponent<Animator>();
    }

    public void BossTakeDamage(float damageAmount)
    {
        if (health <= maxHealth / 2 && !isSecondStage)
        {
            EnterSecondStage();
        }
        if (isInvincible)
        {
            return;
        }
        health -= damageAmount;

        bossAnimator.SetTrigger("Hit");


        UpdateHealthBar();

        audioSource.PlayOneShot(hitSound, 1f);


        if (health <= 0)
        {

            bossAnimator.SetTrigger("Defeated");


            healthSlider.gameObject.SetActive(false);

            Destroy(gameObject, 2f);

            firstGate.OpenGate();
            secondGate.OpenGate();

            audioSource.Pause();

            victoryAudio.Play();


        }
        else
        {
            StartInvincibility();
        }

    }

    public void EnterSecondStage()
    {
        isSecondStage = true;

        transform.localScale = enlargedScale;
    }

    private void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
        bossMovement.SetInvincible(true);
        spriteRenderer.color = Color.red;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0)
            {
                isInvincible = false;

                bossMovement.SetInvincible(false);

                spriteRenderer.color = Color.white;
            }
        }
    }
    private void UpdateHealthBar()
    {
        healthSlider.value = health;
    }

    public bool IsInvincible()
    {
        return isInvincible;
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
        if (other.CompareTag("Player"))
        {
            if (!isInvincible)
            {
                BossTakeDamage(1);
            }
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50));

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
}








 
        

