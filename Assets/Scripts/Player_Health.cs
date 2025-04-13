using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{

    [Header("PlayerHealthInfo")]
    public int maxHealth = 100; 
    private int currentHealth; 

    [Header("AutoHeal")]

    public float healingTime = 2f; 
    public int healingAmount = 2;  
    private float timeSinceLastDamage = 0f; 

    public Slider healthSlider;  

    public static GameManagerScript Instance;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth; 
        animator = GetComponent<Animator>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; 
            healthSlider.value = currentHealth; 
            healthSlider.interactable = false; 
        }

        StartCoroutine(AutoHeal()); 
    }

    void Update()
    {
        if (currentHealth < maxHealth)
        {
            timeSinceLastDamage += Time.deltaTime;  
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
        GameManagerScript.Instance.gameOver();  
    }

   
    private System.Collections.IEnumerator AutoHeal()
    {
        while (true) 
        {
            if (timeSinceLastDamage >= healingTime && currentHealth < maxHealth)
            {
                Heal(healingAmount);  
            }
            yield return new WaitForSeconds(0.2f);  
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount; 
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; 
        } 

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;  
        }
    }
}