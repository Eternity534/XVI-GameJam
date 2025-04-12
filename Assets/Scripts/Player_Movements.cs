using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    private bool facingRight = true;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 20f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public float jumpTimeMax = 0.3f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sharedMaterial = null; // Assurez-vous qu'il n'y a pas de friction via le Physics Material
    }

    void Update()
    {
        // --- Détection du sol ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- Entrée du joueur ---
        moveInput = Input.GetAxisRaw("Horizontal");

        // --- Saut initial ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTimeMax;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // --- Maintien du saut ---
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // --- Fin du saut ---
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        // --- Mouvement horizontal glissant (pas de friction) ---
        float targetVelocityX = moveInput * moveSpeed;
        float velocityX = Mathf.Lerp(rb.linearVelocity.x, targetVelocityX, acceleration * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}