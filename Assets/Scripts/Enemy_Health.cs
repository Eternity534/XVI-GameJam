using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int health = 20;
    public int damage = 10;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Health playerHealth = collision.gameObject.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        if (collision.gameObject.CompareTag("Ally"))
        {
            Invocation invocationHealth = collision.gameObject.GetComponent<Invocation>();
            if (invocationHealth != null)
            {
                invocationHealth.TakeDamage(damage);
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}