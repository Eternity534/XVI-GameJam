using UnityEngine;
using UnityEngine.Tilemaps;

public class BossController : MonoBehaviour
{
    public GameObject bossProjectilePrefab;
    public Transform[] firePoints;
    public float shootInterval = 2f;

    public int maxHealth = 100;
    private int currentHealth;

    private float shootTimer;

    [Header("Collision Damages")]
    public int damagePerTick = 10;
    public float damageInterval = 1f;

    private bool playerInContact = false;
    private Coroutine damageCoroutine;

    [Header("Zone de mouvement")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    [Header("Mouvement")]
    public float moveSpeed = 10f;
    public float waitTimeBetweenMoves = 1f;

    private Vector2 targetPosition;
    private bool isMoving = false;

    [Header("Mur à ouvrir après la mort du boss")]
    public Tilemap wallTilemap;
    public Vector3Int[] wallTilesToRemove;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerInContact = true;
            damageCoroutine = StartCoroutine(DamageOverTime(collision.collider.GetComponent<Player_Health>()));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerInContact = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private System.Collections.IEnumerator DamageOverTime(Player_Health playerHealth)
    {
        while (playerInContact && playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(damageInterval);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        shootTimer = shootInterval;
        PickNewTargetPosition();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            ShootProjectile();
            shootTimer = shootInterval;
        }

        if (!isMoving)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    public void ActivateBoss()
    {
        Debug.Log("Boss Active");

        currentHealth = maxHealth;
        BossHealthUI.Instance.Show();
        BossHealthUI.Instance.UpdateHealth(currentHealth, maxHealth);
    }

    void ShootProjectile()
    {
        foreach (Transform point in firePoints)
        {
            Instantiate(bossProjectilePrefab, point.position, point.rotation);
        }
    }

    void PickNewTargetPosition()
    {
        float targetX = Random.Range(minBounds.x, maxBounds.x);
        float targetY = Random.Range(minBounds.y, maxBounds.y);
        targetPosition = new Vector2(targetX, targetY);
    }

    System.Collections.IEnumerator MoveToTarget()
    {
        isMoving = true;

        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(waitTimeBetweenMoves);
        PickNewTargetPosition();
        isMoving = false;
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
        if (wallTilemap != null)
        {
            foreach(Vector3Int pos in wallTilesToRemove)
            {
                wallTilemap.SetTile(pos, null);
            }
        }
        Destroy(gameObject);
    }
}
