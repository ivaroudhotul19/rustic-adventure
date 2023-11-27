using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth; 
    private GameCtrl gameCtrl;

    public float speed;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    private bool isHitByPowerBullet = false;

    private const int IDLE_STATE = 0;
    private const int HIT_STATE = 1;
    private const int SEVERE_HURT_STATE = 2;
    private const int DEATH_STATE = 3;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        SetStartingDirection();
        gameCtrl = GameCtrl.instance;
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Update() {
        if (currentHealth == 0)
        {
            StartCoroutine(DelayedBulletHitEnemy());
        } 
        else if (currentHealth == 1)
        {
            StartCoroutine(ResetHitByPowerBulletFlagSevereHurt());
        }

        if (!isHitByPowerBullet)  // Only move if not hit by a power bullet
        {
            Move();
            anim.SetInteger("State", IDLE_STATE);
        }
    }

    void Move(){
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }

    void SetStartingDirection(){
        sr.flipX = speed < 0;
    }

    void FlipOnCollision(){
        speed = -speed;
        SetStartingDirection();
    }

    IEnumerator ResetHitByPowerBulletFlag()
    {
        isHitByPowerBullet = true;
        anim.SetInteger("State", HIT_STATE);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.0f);
        isHitByPowerBullet = false;
    }

    IEnumerator ResetHitByPowerBulletFlagSevereHurt()
    {
        isHitByPowerBullet = true;
        anim.SetInteger("State", HIT_STATE);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator DelayedBulletHitEnemy()
    {
        anim.SetInteger("State", DEATH_STATE);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        GameCtrl.instance.BulletHitEnemy(transform);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth == 0)
        {
            StartCoroutine(DelayedBulletHitEnemy());
        } 
        else if (currentHealth == 1)
        {
            StartCoroutine(ResetHitByPowerBulletFlagSevereHurt());
        }
        else 
        {
            StartCoroutine(ResetHitByPowerBulletFlag());
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Powerup_Bullet"))
        {
            //
        }
        else if (!other.gameObject.CompareTag("Player"))
        {
            FlipOnCollision();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            gameCtrl.ReducePlayerHealthMushmaw();
        }
    }
}
