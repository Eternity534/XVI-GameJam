using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy_Movements : MonoBehaviour
{
    public float speed = 2f;  
    public float moveDistance = 1.5f;  
    private Vector2 startPosition;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool movingRight = true;
    private bool isGrounded;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sharedMaterial = null; 
        startPosition = transform.position; 
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        Move();
    }

    void Move()
    {
        float movement = movingRight ? speed * Time.deltaTime : -speed * Time.deltaTime;
        transform.Translate(movement, 0, 0);

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            movingRight = !movingRight; 
        }
    }
}