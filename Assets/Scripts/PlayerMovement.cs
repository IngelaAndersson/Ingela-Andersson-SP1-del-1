using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Plats för att lagra referensen från Unity.
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip jumpSound, pickupSound;
    [SerializeField] private GameObject appleParticles, dustParticles;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color greenHealth, redHealth;
    [SerializeField] private TMP_Text appleText;

    private float horizontalValue;
    private float rayDistance = 0.25f;
    private bool isGrounded;
    private bool canMove;
    private int startingHealth = 5;
    public int currentHealth = 0;
    public int applesCollected = 0;
    private string sceneName;
    

    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        canMove = true;
        currentHealth = startingHealth;

        appleText.text = "" + applesCollected;

        //Denna går in på objektet (Player), letar efter en komponent av typen Rigidbody2D. Sparas i variabeln rgbd.
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if(horizontalValue < 0)
        {
            FlipSprite(true);
        }

        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }


        //Vill bara hoppa när vi trycker på knappen, ej fortsätta när knappen är nedtryckt.
        //Kollar om spelaren trycker på jump. Trycker vi på spacebar går den till jump-funktionen
        //Den går till CheckIfGrounded funktionen för att se om någon av fötterna nuddar Ground annars
        //går det ej att hoppa. 
        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true) 
        {
            Jump();
        }

        //Kollar hastigheten vänster & höger, upp, ned och om vi står på marken eller ej.
        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.velocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());

    }

    //Används för att få samma resultat oavsett dator. 
    private void FixedUpdate()
    {
        if (!canMove)
        {
            //Om false går den ej vidare till raden under inom FixedUpdate.
            return;
        }

        //Vector är en samling av 3 värden (x, y, z). Måste användas vid velocity också.
        //Den läser från horizontalValue vilket värde vi trycker på. I y-led låter den karaktären falla.
        rgbd.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            applesCollected++;
            appleText.text = "" + applesCollected;
            audioSource.PlayOneShot(pickupSound, 0.1f);
            //Instantiate klonar ett object.
            Instantiate(appleParticles, other.transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject);
        }   

    }

    //Void innebär att när metoden kallats på och körts så returnerar den ingenting. Om den bara ska utföra något kan man skriva void.
    //Annars kan void användas för att returnera värden.
    private void FlipSprite(bool direction)
    {
        rend.flipX = direction; 
    }

    //Jump-funktionen. Säger åt rigidbody att ge kraft uppåt (y-axeln). 
    private void Jump()
    {
        rgbd.AddForce(new Vector2(0, jumpForce));
        //Spelar upp hopp-ljudet
        audioSource.PlayOneShot(jumpSound, 0.5f);
        Instantiate(dustParticles, transform.position, dustParticles.transform.localRotation);
    }


    //Tar bort ett liv
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    public void TakeKnockback(float knockbackForce, float upwards)
    {
        canMove = false;
        rgbd.AddForce(new Vector2(knockbackForce, upwards));
        //Invoke använder man för att göra delay.
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    { 
        canMove = true;
    }

    private void Respawn()
    {
        if(sceneName != "Level3")
        {
            currentHealth = startingHealth;
            UpdateHealthBar();
            transform.position = spawnPosition.position;
            rgbd.velocity = Vector2.zero;
        }
    }

    private void RestoreHealth(GameObject healthPickup)
    {
        //Om vi har full health händer ingenting
        if (currentHealth >= startingHealth)
        {
            return;
        }
        //Om vi har lägre än full health adderar vi, uppdaterar UI & förstör pickup.
        else
        {
            int healthToRestore = healthPickup.GetComponent<HealthPickup>().healthAmount;
            currentHealth += 3;
            UpdateHealthBar();
            Destroy(healthPickup);

            if(currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        if (currentHealth >= 2)
        {
            fillColor.color = greenHealth;
        }
        else
        {
            fillColor.color = redHealth;
        }
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

        //Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);
        //Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);

        //Om raycasten har träffat något, alltså att den ej är noll, OCH det den träffar är taggat som Ground
        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
