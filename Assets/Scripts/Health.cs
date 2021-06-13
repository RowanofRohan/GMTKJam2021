using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
        if (transform.CompareTag("Player"))
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Destroy(Instantiate(deathEffect.gameObject, transform.position, Quaternion.identity), 5f);
            
        }

        if (transform.CompareTag("Player"))
        {
            healthBar.SetHealth(currentHealth);
        }
    }
}
