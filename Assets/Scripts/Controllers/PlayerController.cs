using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 4f;
    public float jumpForce = 7f;
    public float longIdleDelay = 5f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private float idleTimer;
    private bool hasPlayedLongIdle;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        idleTimer = 0f;
        hasPlayedLongIdle = false;
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Rotate sprite
        if (moveInput > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Animations
        animator.SetFloat("movement", Mathf.Abs(moveInput));
        animator.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0);

        //Idle 
        if (Mathf.Abs(moveInput) < 0.1f && isGrounded)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= longIdleDelay && !hasPlayedLongIdle)
            {
                animator.SetTrigger("LongIdle");
                hasPlayedLongIdle = true;
            }
            else if (hasPlayedLongIdle)
            {
                hasPlayedLongIdle = false;
                animator.ResetTrigger("LongIdle");
                idleTimer = 0f;
            }
        }
        else
        {
            idleTimer = 0f;
            hasPlayedLongIdle = false;
            animator.ResetTrigger("LongIdle");
        }

        // Coyote time
        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("isFalling", false);
        }

        // Jump with coyote time
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Check ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}