using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovementTikus : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    private GameCtrl gameCtrl;

    public float speed;
    public float moveDistance;  // Jarak pergerakan keseluruhan
    private float initialPositionX;  // Posisi awal karakter
    private float targetPositionX;  // Posisi tujuan berikutnya
    private bool movingRight = true;  // Arah pergerakan saat ini

    private GameObject player;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    private bool isHitByPowerBullet = false;
    private bool isChasingPlayer = false;

    private const int diam = 0;
    private const int berjalan = 1;
    private const int menyerang = 2;
    private const int mati = 3;

    public Transform FloatingTextPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gameCtrl = GameCtrl.instance;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        initialPositionX = transform.position.x;  // Simpan posisi awal karakter
        SetNextTarget();
    }

    void Update()
    {
        if (currentHealth == 0)
        {
            //StartCoroutine(DelayedBulletHitEnemy());
            Invoke("DelayedBulletHitEnemy", 0.1f);
        }
        else if (currentHealth == 1)
        {
            StartCoroutine(ResetHitByPowerBulletFlagSevereHurt());
        }

        if (!isHitByPowerBullet)  // Only move if not hit by a power bullet
        {
            if (!isChasingPlayer)
            {
                // Bergerak ke kanan atau kiri dengan kecepatan tetap
                Move();
                UpdateAnimationState();
            }
        }
    }

    void Move()
    {
        float horizontalInput = Mathf.Sign(targetPositionX - transform.position.x) * 1.0f;

        // Bergerak ke kanan atau kiri
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Periksa apakah mencapai target posisi
        if (Mathf.Abs(transform.position.x - targetPositionX) < 0.1f)
        {
            SetNextTarget();  // Set posisi tujuan berikutnya setelah mencapai target
        }

    }

    void SetNextTarget()
    {
        // Tentukan posisi tujuan berikutnya berdasarkan arah pergerakan saat ini
        targetPositionX = movingRight ? initialPositionX + moveDistance : initialPositionX - moveDistance;
        movingRight = !movingRight;  // Ubah arah pergerakan
        SetStartingDirection();  // Tetapkan arah karakter sesuai dengan perubahan arah
    }

    void SetStartingDirection()
    {
        // Tetapkan arah awal karakter sesuai dengan arah pergerakan
        sr.flipX = movingRight;
    }

    void UpdateAnimationState()
    {
        // Atur animasi ke mode berjalan
        anim.SetInteger("State", berjalan);
    }

    IEnumerator ResetHitByPowerBulletFlag()
    {
        if (!isChasingPlayer)
        {
            isHitByPowerBullet = true;
            anim.SetInteger("State", menyerang);
            rb.velocity = Vector2.zero;
            StartCoroutine(ChasePlayer());
        }

        yield return new WaitForSeconds(5.0f);

        sr.flipX = movingRight;
        isHitByPowerBullet = false;
    }

    IEnumerator ResetHitByPowerBulletFlagSevereHurt()
    {
        isHitByPowerBullet = true;
        anim.SetInteger("State", diam);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator DelayedBulletHitEnemy()
    {
        anim.SetInteger("State", mati);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        GameCtrl.instance.BulletHitEnemy(transform);
    }

    IEnumerator ChasePlayer()
    {
        if (!isChasingPlayer)
        {
            isChasingPlayer = true;
            while (isChasingPlayer && Vector2.Distance(transform.position, player.transform.position) > 0.5f)
            {
                // Hitung arah menuju pemain
                Vector2 direction = (player.transform.position - transform.position).normalized;

                // Bergerak ke arah pemain
                rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

                // Tetapkan arah karakter sesuai dengan perubahan arah
                sr.flipX = direction.x < 0;

                yield return null;
            }

            // Setelah selesai menyerang, hentikan karakter
            rb.velocity = Vector2.zero;
            isChasingPlayer = false;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        int currentDamage = (int)damageAmount;

        PoinPopup poinPopup = PoinPopup.Create(transform.position, currentDamage, FloatingTextPrefab);
        if (poinPopup != null)
        {
            poinPopup.SetPosition(transform.position + new Vector3(0f, 1f, 0f));
        }

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

    IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(0.1f);
        Move();
        UpdateAnimationState();
    }

    void ResetChase()
    {
        StopCoroutine("ChasePlayer");
        isChasingPlayer = false;
        isHitByPowerBullet = false;
        sr.flipX = movingRight;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Powerup_Bullet"))
        {
            // Handle collision with power-up bullet if needed
        } 
        else if (other.gameObject.CompareTag("Player"))
        {
            gameCtrl.ReducePlayerHealthTikus();
        } else if (other.gameObject.CompareTag("Barrier")) {
            ResetChase();
            StartCoroutine(DelayedMove());
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            // Handle collision with water
            StartCoroutine(DelayedBulletHitEnemy());
            SFXCtrl.instance.ShowSplash(other.gameObject.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            ResetChase();
            StartCoroutine(DelayedMove());
        }
    }
}
