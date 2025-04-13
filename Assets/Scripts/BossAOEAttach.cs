using UnityEngine;

public class BossAOEAttack : MonoBehaviour
{
    public int damage = 40;
    public float duration = 2f; 
    public float damageInterval = 0.5f;
    private float timer = 0f;
    private float damageTimer = 0f;

    private void Start()
    {
        timer = duration;
        damageTimer = damageInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject); 
        }
        damageTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Health playerHealth = other.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (damageTimer <= 0f && other.CompareTag("Player"))
        {
            Player_Health playerHealth = other.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            damageTimer = damageInterval; 
        }
    }
}