using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public int[] demonSpawnTargets = { 10, 70, 110 };
    public Ghost ghost;
    public bool faceLeft = true;
    public GameObject demon;
    public GameObject enemyHealthBar;
    public bool moveLeft = false;
    public bool moveRight = false;

    private Vector2 move;
    private bool jump = false;
    private bool fire = false;

    private Enemy enemy;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform firePoint;
    private Weapon weapon;
    //private int spawnTargetsIndex = 0;
    private bool demonSpawned = false;


    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        weapon = GetComponent<Weapon>();
        enemy = demon.GetComponent<Enemy>();

        firePoint = this.transform.Find("FirePoint");

        demon.SetActive(false);
        enemyHealthBar.SetActive(false);

        Score.score = 0;
    }


    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();

        // if (currProjectile < totalProjectile)
        // {
        //     if (currPorjectileWaitTime <= 0)
        //     {
        //         currProjectile++;
        //         currPorjectileWaitTime = totalProjectileWaitTime;
        //     }
        //     else
        //     {
        //         currPorjectileWaitTime -= Time.deltaTime;
        //     }
        // }

        // UIBlastsBar.instance.SetValue(currProjectile / (float)totalProjectile);

        //TODO: spawn demons at target scores
        // if (spawnTargetsIndex < demonSpawnTargets.Length)
        // {
        //     if (Score.score >= demonSpawnTargets[spawnTargetsIndex])
        //     {
        //         Debug.Log("array length: " + demonSpawnTargets.Length);
        //         Debug.Log("spawntarget index is: " + spawnTargetsIndex);
        //         SpawnDemon(40, 1);
        //         spawnTargetsIndex++;
        //     }
        // }

        if (Score.score >= 30 && demonSpawned == false)
        {
            SpawnDemon(100, 20);
            demonSpawned = true;
        }

    }

    private void SpawnDemon(int health, int damage)
    {
        demon.SetActive(true);
        enemyHealthBar.SetActive(true);

        //TODO: Change later
        //enemy.updateStats(health, damage);

    }

    protected override void ComputeVelocity()
    {

        move = Vector2.zero;

        float moveDir = Input.GetAxis("Horizontal");
        if (moveLeft)
        {
            move.x = -1;
        }
        if (moveRight)
        {
            move.x = 1;
        }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     JumpBtn();
        // }
        // else if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     JumpBtnRelease();
        // }



        //if (Input.GetButtonDown("Jump") && grounded)
        if (jump && grounded)
        {

            animator.SetTrigger("TakeOff");
            velocity.y = jumpTakeOffSpeed;
            jump = false;

        }
        // else if (Input.GetButtonUp("Jump"))
        // {
        //     if (velocity.y > 0)
        //     {
        //         velocity.y = velocity.y * .5f;
        //     }
        // }

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     fire = true;
        // }
        if (fire)
        {
            // If grounded, will trigger projectile animation which, 
            // if interupted, will never call Shoot and decrease projectiles
            // Otherwise, will Shoot right away
            FindObjectOfType<AudioManager>().Play("SpellLaunch");
            if (grounded)
            {
                animator.SetTrigger("Projectile");
            }
            else
            {
                weapon.Shoot();
            }
            fire = false;
        }

        if (grounded == false)
        {
            animator.SetBool("IsJumping", true);
            ghost.makeGhost = true;
        }
        else
        {

            animator.SetBool("IsJumping", false);
            animator.SetFloat("Speed", Mathf.Abs(velocity.x) / maxSpeed);
            ghost.makeGhost = false;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x < -0.01f) : (move.x > 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            firePoint.transform.Rotate(0f, 180f, 0f);

        }



        targetVelocity = move * maxSpeed;

    }

    public void JumpBtn()
    {
        if (grounded)
            jump = true;
    }

    public void JumpBtnRelease()
    {
        if (!grounded)
        {
            velocity.y = velocity.y * .5f;
        }
    }

    public void FireBtn()
    {
        fire = true;
    }

    public void MoveLeftBtn()
    {
        moveLeft = true;
    }

    public void MoveRighttBtn()
    {
        moveRight = true;
    }

}
