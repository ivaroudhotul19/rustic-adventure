using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public float jumpSpeed;
    public int startJumpingAt;
    public int jumpDelay;
    public int health;
    public Slider bossHealth;
    public GameObject bossBullet;
    public GameObject bossBullet2;
    public float delayBeforeFiring;
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    // Vector3 bulletSpawnPos;
    Animator anim;
    bool canFire, isJumping;
    bool isWalking; // New variable to track walking state
    public float walkSpeed; // Adjust the speed to your liking

    public Transform bulletSpawnPos, bulletSpawnPosRight;

    private const int diam = 0;
    private const int berjalan = 1;
    private const int menyerang = 2;
    private const int menghindar = 3;
    private const int mati = 4;
    public int initialHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        canFire = false;
        isWalking = false;

        Invoke("Reload", Random.Range(1f, delayBeforeFiring));
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) 
        {
            // Trigger jumping when the player shoots
            if (!isJumping)
            {
                Jump();
            }
        }
        if (canFire)
        {
            FireBullets();
            canFire = false;

            // Add walking logic here with a distance check
            if (!isJumping && !isWalking)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    float distance = Vector2.Distance(transform.position, player.transform.position);

                    Debug.Log("Distance: " + distance);

                    // Adjust the threshold distance as needed
                    if (distance < 5.0f)
                    {
                        isWalking = true;
                        anim.SetInteger("State", berjalan);
                        // Use a coroutine for smooth walking animation
                        StartCoroutine(WalkToPlayer());
                    }
                }
            }
        }
    }


    void FireBullets()
    {
        // Set the attack animation state
        anim.SetInteger("State", menyerang);

        if (sr.flipX) {
            Instantiate(bossBullet, bulletSpawnPosRight.position, Quaternion.identity);
        } else if (!sr.flipX) {
            Instantiate(bossBullet2, bulletSpawnPos.position, Quaternion.identity);
        }
        Invoke("Reload", delayBeforeFiring);
        // Delay to allow for the attack animation before resetting to other states
        Invoke("ResetAnimationState", 0.1f);
    }

    void ResetAnimationState()
    {
        // Reset the animation state to walking after firing
        if (!isJumping)
        {
            anim.SetInteger("State", diam);
        }
    }

    IEnumerator WalkToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            while (distance > 0.1f)
            {
                // Determine the direction to the player
                Vector2 direction = (player.transform.position - transform.position).normalized;

                // Flip the sprite based on the direction
                if (direction.x > 0)
                    sr.flipX = false; // facing right
                else if (direction.x < 0)
                    sr.flipX = true;  // facing left

                // Move towards the player
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, walkSpeed * Time.deltaTime);

                // Update the distance for the next frame
                distance = Vector2.Distance(transform.position, player.transform.position);

                // Wait for the next frame
                yield return null;
            }
        }

        // Stop walking
        isWalking = false;
    }

    void Reload()
    {
        canFire = true;
    }

    void Jump()
    {
        // Set the jumping animation state
        anim.SetInteger("State", menghindar);

        // Delay before resetting to walking state
        Invoke("ResetAnimationState", 0.5f);

        rb.AddForce(new Vector2(0, jumpSpeed));
    }

    void RestoreColor()
    {
        sr.color = Color.white;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Powerup_Bullet"))
        {
            if (health == 0)
            {
                anim.SetInteger("State", mati);
                // Delay before handling the enemy being hit
                Invoke("HandleEnemyHit", 1.0f);
            }

            if (health > 0)
            {
                health--;
                bossHealth.value = health;

                sr.color = Color.red;

                Invoke("RestoreColor", 0.1f);
            }
        }
    }

    void HandleEnemyHit()
    {
        // Call a method to handle the enemy being hit
        GameCtrl.instance.ShowHarmonyKey(gameObject.transform);
    }

    public void SetBossHealth(int newHealth)
    {
        health = newHealth;
        bossHealth.value = health;
    }
}
