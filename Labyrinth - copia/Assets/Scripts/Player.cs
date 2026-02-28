using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public int coins;
    public int collectables;
    public int health = 100;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Image healthImage;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Animator animator;

    public int extraJumpsValue = 1;
    private int extraJumps;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if(rb.linearVelocityX != 0)
        {
            if(rb.linearVelocityX > 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
                Debug.Log("extraJump"); // Mira sin funcionan los saltos extras.
            }
        }

        SetAnimation(moveInput);

        healthImage.fillAmount = health / 100f;

        if (transform.position.y < -100)
        {
            Die();
        }


    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // Debug.Log("IsGrounded"); // Mirar si está tocando el suelo. 
    }


    private void SetAnimation(float moveImput)
    {
        if (isGrounded)
        {
            if (moveImput == 0)
            {
                animator.Play("Player_Idle");
            }
            else
            {
                animator.Play("Player_Run");
            }
        }
        else
        {
            if (rb.linearVelocityY > 0)
            {
                animator.Play("Player_Jump");
            }
            else
            {
                animator.Play("Player_Fall");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            health -= 10;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());
            Debug.Log("Damage"); // Muesta cuando te hace daño.

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
