using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Invocation : MonoBehaviour
{
    public float moveSpeed = 3f;        
    public float attackRange = 3f;    
    public int health = 15;            
    public int damage = 10;   
    public float damageInterval = 1f; 
    public float lifetime = 1f; 

    private Transform targetEnemy; 
    private Vector3 startPosition; 
    private bool isMoving = true;
    private float currentTime = 0f; 

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    void Start()
    {
        startPosition = transform.position;
        InvokeRepeating("FindEnemyAndAttack", 0f, 0.5f);
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isMoving)
        {
            MoveForward();
        }

        currentTime += Time.deltaTime;

        if (targetEnemy != null)
        {
            AttackEnemy();
        }
    }

    void FindEnemyAndAttack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        targetEnemy = closestEnemy;
    }

    void MoveForward()
    {
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled < 10f)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;  
        }
    }

    void AttackEnemy()
    {
        if (targetEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);
            if (distanceToEnemy <= attackRange && currentTime >= damageInterval)
            {
                Enemy_Health enemyHealth = targetEnemy.GetComponent<Enemy_Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
                currentTime = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (currentTime >= damageInterval)
            {
                Enemy_Health enemy = collision.gameObject.GetComponent<Enemy_Health>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                currentTime = 0f;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}