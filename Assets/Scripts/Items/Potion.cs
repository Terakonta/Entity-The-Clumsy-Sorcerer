using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    public int health = 10;
    public float timer = 5;
    public ParticleSystem potionEffect;

    private Animator animator;
    private PlayerHealth playerHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            Destroy(gameObject);

            FindObjectOfType<AudioManager>().Play("Potion");
            playerHealth.AddHealth(health);

            Instantiate(potionEffect, other.transform.position, Quaternion.identity);


        }
    }
}
