using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 5f;
    public int damage = 10;

    private Vector2 direction;

    void Start()
    {
        direction = Vector2.left; 
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player_Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
