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

    void Update()
    {
        if (Input.GetKeyDown(invocationKey))
        {
            InvoquerAlly();
        }

        if (SceneManager.GetActiveScene().name == "Hell_Test") 
        {
            if (Input.GetKeyDown(shootKey))
            {
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