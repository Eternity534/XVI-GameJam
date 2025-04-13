using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAbility : MonoBehaviour
{
    [Header("Invocation")]
    public GameObject allyPrefab;
    public Transform spawnPoint;
    public KeyCode invocationKey = KeyCode.C;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public KeyCode shootKey = KeyCode.X;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(invocationKey))
        {
            InvoquerAlly();
        }

        if (SceneManager.GetActiveScene().name == "Tuto") 
        {
            if (Input.GetKeyDown(shootKey))
            {
                animator.SetBool("Attack", Input.GetKey(shootKey));
                Shoot();
            }
        }
    }

    void InvoquerAlly()
    {
        Instantiate(allyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void Shoot()
    {
        Debug.Log("Tir !");
        Vector2 shootDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(shootDirection);
    }
}