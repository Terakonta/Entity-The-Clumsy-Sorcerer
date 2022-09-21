using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public Rigidbody2D rb;
    public int damage = 10;
    public ParticleSystem damageEffect;

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 25f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            FindObjectOfType<AudioManager>().Play("FireSound");

            player.TakeDamage(damage);
            Instantiate(damageEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
