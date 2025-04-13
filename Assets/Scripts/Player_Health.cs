using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth; 

    public Slider healthSlider; 

    public GameManagerScript gameManager;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth; 
        animator = GetComponent<Animator>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; 
            healthSlider.value = currentHealth; 
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        if (currentHealth < 0)
            currentHealth = 0; 

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; 
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        animator.SetBool("Death", true);
        gameManager.gameOver();
    }

    /*
    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    */

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth; 

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; 
        }
    }
}
