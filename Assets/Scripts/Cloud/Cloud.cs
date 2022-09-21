using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public int damage = 10;
    public int cloudScore = 5;
    public ParticleSystem cloudEffect;
    public GameObject potion;

    private bool isFlipped = false;

    Animator animator;
    GameObject player;
    PlayerHealth playerHealth;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Entity");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerHealth = other.GetComponent<PlayerHealth>();
        Projectile projectile = other.GetComponent<Projectile>();
        if (playerHealth != null)
        {
            FindObjectOfType<AudioManager>().Play("CloudDieSound");

            playerHealth.TakeDamage(damage);
            Instantiate(cloudEffect, player.transform.position, Quaternion.identity);

        }
        else if (projectile != null)
        {
            Score.score += cloudScore;

            // Drop potion 30% of the time
            int dropRate = Random.Range(1, 10);

            if (dropRate <= 3)
            {
                Instantiate(potion, gameObject.transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    public void CloudLookAtPlayer()
    {
        //Vector3 flipped = transform.localScale;
        //flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            //transform.localScale = flipped;
            //transform.Rotate(0f, 180f, 0f);
            spriteRenderer.flipX = false;
            isFlipped = false;

        }
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            //transform.localScale = flipped;
            //transform.Rotate(0f, 180f, 0f);
            spriteRenderer.flipX = true;
            isFlipped = true;

        }
    }
}
