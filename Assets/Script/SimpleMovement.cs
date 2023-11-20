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
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        SetStartingDirection();
        gameCtrl = GameCtrl.instance;
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Update() {
        Move();
        
    }

    void Move(){
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
        anim.SetInteger("State", 1);
    }

    void SetStartingDirection(){
        if(speed < 0) {
            sr.flipX = true;
        } else if(speed > 0) {
            sr.flipX = false;
        }
    }

    void FlipOnCollision(){
        speed = -speed;
        SetStartingDirection();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth == 0)
        {
            GameCtrl.instance.BulletHitEnemy(transform);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(!other.gameObject.CompareTag("Player")){
            FlipOnCollision();
        } else {
            if (gameCtrl != null){
               gameCtrl.ReducePlayerHealthMushmaw();
            }
        }
    }
}