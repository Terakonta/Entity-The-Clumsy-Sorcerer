using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 10;
    public float distance;

    public GameObject player;

    private float currentDistance;
    public Rigidbody2D rb;
    public ParticleSystem particleHitEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        if (transform.position.magnitude > 25f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Cloud cloud = collision.GetComponent<Cloud>();
        if (enemy != null)
        {
            FindObjectOfType<AudioManager>().Play("ProjectileSound");

            enemy.TakeDamage(damage);
            Instantiate(particleHitEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (cloud != null)
        {
            FindObjectOfType<AudioManager>().Play("ProjectileSound");

            Instantiate(particleHitEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
