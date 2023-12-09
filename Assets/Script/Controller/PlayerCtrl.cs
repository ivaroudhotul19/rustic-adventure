using System.Collections;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
	public float speedBoots; //set 5

	public float jumpSpeed;// set 600
	public bool isGrounded;
	public Transform feet;
	public float feetRadius;
	public float boxHeight;
	public float boxWidth;
	public float delayForDoubleJump;
	public LayerMask whatIsGrounded;
	public Transform leftBulletSpawnPos, rightBulletSpawnPos;
	public GameObject leftBullet, rightBullet;
	public bool SFXOn;
	public bool canFire;
	private Vector3 initialPosition;

	Rigidbody2D rb;
	SpriteRenderer sr;
	Animator anim;
	public bool isJumping, canDoubleJump;
	bool leftPressed, rightPressed;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator> ();
	}

	void Update()
	{
		isGrounded = Physics2D.OverlapCircle (feet.position, feetRadius, whatIsGrounded);

		isGrounded = Physics2D.OverlapBox (new Vector2 (feet.position.x, feet.position.y), new Vector2 (boxWidth, boxHeight), 360.0f, whatIsGrounded);
		float playerSpeed = Input.GetAxisRaw("Horizontal") * speedBoots;

		if (playerSpeed != 0) {
			MoveHorizontal(playerSpeed);
		} else {
			StopMoving();
		}

		if (Input.GetButtonDown("Jump")) {
			Jump();
		}

		if (Input.GetButtonDown ("Fire1")) {
			FireBullets ();
		}
		ShowFalling ();

		if (leftPressed)
			MoveHorizontal (-speedBoots);
	
		if (rightPressed)
			MoveHorizontal (speedBoots);
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (feet.position, feetRadius);
		Gizmos.DrawWireCube (feet.position, new Vector3 (boxWidth, boxHeight, 0));
	}

	void MoveHorizontal(float playerSpeed) {
		rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
		if (playerSpeed < 0)
			sr.flipX = true;
		else if (playerSpeed > 0)
			sr.flipX = false;	
		if(!isJumping)
			anim.SetInteger ("State", 1);
	}

	void StopMoving()
	{
		rb.velocity = new Vector2(0, rb.velocity.y);
		if(!isJumping)
			anim.SetInteger ("State", 0);
	}

	void ShowFalling(){
		if (rb.velocity.y < 0) {
			anim.SetInteger ("State", 3);
		}
	}

	void Jump(){
		if (isGrounded) {
			isJumping = true;
			rb.AddForce (new Vector2 (0, jumpSpeed));
			anim.SetInteger ("State", 2);

			AudioCtrl.instance.PlayerJump(gameObject.transform.position);

			Invoke ("EnableDoubleIsJump", delayForDoubleJump);
		}

		if (canDoubleJump && !isGrounded && rb.velocity.y < 0) {
			float currentJumpHeight = transform.position.y - initialPosition.y;
			if (currentJumpHeight < 10f) // Batasan tinggi lompatan
			{
				rb.velocity = new Vector2(rb.velocity.x, 0); // Reset kecepatan vertikal
				rb.AddForce(new Vector2(0, jumpSpeed));
				anim.SetInteger("State", 2);

				AudioCtrl.instance.PlayerJump(gameObject.transform.position);

				canDoubleJump = false;
			}
		}
	}
	
	void EnableDoubleIsJump() {
		canDoubleJump = true;
	}

	void FireBullets() {
		if(canFire) {
			//untuk peluru kiri
			if (sr.flipX) {	
				Instantiate (leftBullet, leftBulletSpawnPos.position, Quaternion.identity);	
			}

			//untuk peluru kanan
			if (!sr.flipX) {
				Instantiate (rightBullet, rightBulletSpawnPos.position, Quaternion.identity);	
			}

			AudioCtrl.instance.FireBullets(gameObject.transform.position);
		}
	}
	public void MobileMoveLeft () {
		leftPressed = true;
	}

	public void MobileMoveRight () {
		rightPressed = true;
	}

	public void MobileStop () {
		leftPressed = false;
		rightPressed = false;

		StopMoving ();
	}

	public void MobileFireBullets() {
		FireBullets ();
	}

	public void MobileJump () {
		Jump ();
	}
	void OnCollisionEnter2D(Collision2D other) {
		// if (other.gameObject.CompareTag ("Ground")) {
		// 	isJumping = false;
		// }
		if (other.gameObject.CompareTag ("Enemy")) {
			GameCtrl.instance.PlayerDiedAnimation(gameObject);
			AudioCtrl.instance.PlayerDied(gameObject.transform.position);
		}
		if (other.gameObject.CompareTag ("ShinningCoin")) {
			GameCtrl.instance.updateCoinCount();
			SFXCtrl.instance.ShowBulletSparkle(other.gameObject.transform.position);
			GameCtrl.instance.updateScore(GameCtrl.Item.ShinningCoin);
			Destroy(other.gameObject);
			AudioCtrl.instance.CoinPickup(gameObject.transform.position);
		}

	}
	
	void OnTriggerEnter2D(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Coin":
				if (SFXOn){
					GameCtrl.instance.updateCoinCount();
					SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);
					GameCtrl.instance.updateScore(GameCtrl.Item.Coin);
					Destroy(other.gameObject);
					AudioCtrl.instance.CoinPickup(gameObject.transform.position);
				}
				break;
			case "Water":
				SFXCtrl.instance.ShowSplash(other.gameObject.transform.position);
				AudioCtrl.instance.WaterSplash(gameObject.transform.position);
				break;
			case "Powerup_Bullet":
				canFire = true;
				Vector3 powerupPos = other.gameObject.transform.position; // Store the position in the 'powerupPos' variable
				AudioCtrl.instance.PowerUp(gameObject.transform.position);
				Destroy(other.gameObject);
				if (SFXOn)
					SFXCtrl.instance.ShowBulletSparkle(powerupPos);
				break;
			default:
				break;
		}
	}
}
