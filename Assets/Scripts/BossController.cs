using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform[] firePoints;
    public float shootInterval = 2f;

    public int maxHealth = 100;
    private int currentHealth;

    private float shootTimer;

    void Start()
    {
        currentHealth = maxHealth;
        shootTimer = shootInterval;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            ShootFireballs();
            shootTimer = shootInterval;
        }
    }

    public void ActivateBoss()
    {
        Debug.Log("Boss Active");

        currentHealth = maxHealth;
        BossHealthUI.Instance.Show();
        BossHealthUI.Instance.UpdateHealth(currentHealth, maxHealth);
    }

    void ShootFireballs()
    {
        foreach (Transform point in firePoints)
        {
            Instantiate(fireballPrefab, point.position, point.rotation);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        BossHealthUI.Instance.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        BossHealthUI.Instance.Hide();
        Destroy(gameObject);
    }
}
