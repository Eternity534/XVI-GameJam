using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class Player_Movements : MonoBehaviour
{
    private bool facingRight = true;

    [Header("Movement")]
    public float defaultMoveSpeed = 5f;
    public float moveSpeed;
    public float acceleration = 20f;

    [Header("Jump")]
    public float defaultJumpForce = 12f;
    public float jumpForce;
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
        rb.sharedMaterial = null; 
    }

    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        moveInput = Input.GetAxisRaw("Horizontal");

        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTimeMax;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        
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

        
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();


        // Speed / Jump Boost Ability

        if (SceneManager.GetActiveScene().name == "Paradise_Test")  
        {
            jumpForce = 20f;
            moveSpeed = 10f; 
            Physics2D.gravity = new Vector2(0, -9f); 
        }
        else
        {
            jumpForce = defaultJumpForce;
            moveSpeed = defaultMoveSpeed;
            Physics2D.gravity = new Vector2(0, -15f);
        }
    }

    void FixedUpdate()
    {
        
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Paradise_Test");
        }
    }
}