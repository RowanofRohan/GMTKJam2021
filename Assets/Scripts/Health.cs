using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public GameObject deathEffect;

    public GameObject gameOverPanel;

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

            if (transform.CompareTag("Player"))
            {
                gameOverPanel.SetActive(true);
            }

            gameObject.SetActive(false);
            Destroy(Instantiate(deathEffect.gameObject, transform.position, Quaternion.identity), 5f);

            if (transform.CompareTag("Player"))
            {
                gameOverPanel.SetActive(true);
            }
        }

        if (transform.CompareTag("Player"))
        {
            healthBar.SetHealth(currentHealth);
            AudioManager.PlayMusic("PlayerDamage");
        }
        else
        {
            AudioManager.PlayMusic("EnemyDamage1");
        }
    }
}
