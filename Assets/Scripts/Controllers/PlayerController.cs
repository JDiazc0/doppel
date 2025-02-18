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

    // Catapult mechanic
    private bool isFreezed;
    private Vector2 storedVelocity;
    private float storedGravity;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        idleTimer = 0f;
        hasPlayedLongIdle = false;
        storedGravity = rb.gravityScale;
    }

    void Update()
    {
        if (isFreezed)
        {
            return;
        }

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

        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("isFalling", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Check ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void FreezePlayer(bool freeze)
    {
        isFreezed = freeze;

        if (freeze)
        {
            storedVelocity = rb.linearVelocity;
            storedGravity = rb.gravityScale;
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = storedGravity;
        }
    }

    public bool IsFreezed()
    {
        return isFreezed;
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