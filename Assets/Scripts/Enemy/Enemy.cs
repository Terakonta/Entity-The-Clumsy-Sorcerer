using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int enemyScore = 30;
    public float projectileSpeed = 50f;

    public int attackDamage = 20;

    public float attackRange = 2.5f;

    public LayerMask attackMask;

    public GameObject enemyHealthBar;
    public Transform[] spawnPoints;
    GameObject player;
    public GameObject fireProjectile;
    public ParticleSystem fireAfterEffect;
    public ParticleSystem demonDeathEffect;

    public bool isFlipped = false;

    private Vector2[] directionVectors;
    private int currHealth;
    SpriteRenderer spriteRenderer;
    Transform flame;
    EnemySpawner enemySpawner;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemySpawner = GameObject.Find("SpawnPoints").GetComponent<EnemySpawner>();
        flame = transform.Find("Flame");
        directionVectors = new Vector2[4] { new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, 0) };
        player = GameObject.Find("Entity");
        currHealth = maxHealth;
    }

    public void updateStats(int health, int damage)
    {
        maxHealth = health;
        currHealth = maxHealth;
        attackDamage = damage;
        //UIEnemyHealthBar.instance.SetValue(-0.9f);

    }

    public void Attack()
    {
        FindObjectOfType<AudioManager>().Play("DemonSound");


        Collider2D collInfo = Physics2D.OverlapCircle(flame.position, attackRange, attackMask);
        if (collInfo != null)
        {
            PlayerHealth playerHealth = collInfo.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
        Instantiate(fireAfterEffect, flame.transform.position + Vector3.down * 0.2f, flame.rotation);
        FireProjectiles();
    }

    public void FireProjectiles()
    {
        for (int i = 0; i < directionVectors.Length; i++)
        {
            GameObject fire = Instantiate(fireProjectile, flame.transform.position, Quaternion.identity);
            Fire fireScript = fire.GetComponent<Fire>();
            fireScript.Launch(directionVectors[i], projectileSpeed);
        }
    }

    public void LookAtPlayer()
    {
        //Vector3 flipped = transform.localScale;
        //flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            //transform.localScale = flipped;
            //transform.Rotate(0f, 180f, 0f);
            spriteRenderer.flipX = false;
            isFlipped = false;

            flame.transform.position -= new Vector3(1, 0, 0);

        }
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            //transform.localScale = flipped;
            //transform.Rotate(0f, 180f, 0f);
            spriteRenderer.flipX = true;
            isFlipped = true;

            flame.transform.position += new Vector3(1, 0, 0);

        }
    }


    public void TakeDamage(int damage)
    {
        currHealth -= damage;

        if (currHealth <= 0)
        {
            currHealth = 0;
            Die();
        }
        UIEnemyHealthBar.instance.SetValue(currHealth / (float)maxHealth);
    }

    void Die()
    {
        Score.score += enemyScore;

        Instantiate(demonDeathEffect, transform.position, Quaternion.identity);

        // Spawn demon at random spawnPoint
        enemyHealthBar.SetActive(false);
        gameObject.SetActive(false);

        enemySpawner.totalWaitTime = 1f;


        // int randomPoint = Random.Range(0, spawnPoints.Length);

        //TODO: Spawn at fixed point for now (Change later)
        //gameObject.transform.position = spawnPoints[0].position;

    }
}
