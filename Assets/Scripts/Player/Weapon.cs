using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int totalProjectile = 3;
    public float totalProjectileWaitTime = 5f;
    public Transform firePoint;
    public GameObject projectilePrefab;

    private int currProjectile;
    private float currPorjectileWaitTime;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        currProjectile = totalProjectile;
        currPorjectileWaitTime = totalProjectileWaitTime;
    }

    private void Update()
    {
        // Timer to fill the Blast bar
        if (currProjectile < totalProjectile)
        {
            if (currPorjectileWaitTime <= 0)
            {
                currProjectile++;
                currPorjectileWaitTime = totalProjectileWaitTime;
            }
            else
            {
                currPorjectileWaitTime -= Time.deltaTime;
            }
        }

        UIBlastsBar.instance.SetValue(currProjectile / (float)totalProjectile);

    }


    public void Shoot()
    {
        if (currProjectile > 0)
        {
            currProjectile--;
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        }
        else
        {
            FindObjectOfType<AudioManager>().Play("SpellFail");

        }

    }
}
