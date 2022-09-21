using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;

    private int currHealth;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        UIHealthBar.instance.SetValue(currHealth / (float)maxHealth);
        animator.SetTrigger("Damage");

        if (currHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int health)
    {
        currHealth += health;
        if (currHealth >= maxHealth)
        {
            currHealth = maxHealth;
        }

        UIHealthBar.instance.SetValue(currHealth / (float)maxHealth);

    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
