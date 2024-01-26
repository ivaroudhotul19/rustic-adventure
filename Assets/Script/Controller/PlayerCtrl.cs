using System.Collections;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
	public float speedBoots; //set 5s

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
	public Transform targetPeople;
	public float speed;
	private bool isSizeIncreasing = false;

	public bool isFoundPeople = false;

	public Transform FloatingTextPrefab;
	public GameObject btnFire;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator> ();
	}

	void Update()
	{
		isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, whatIsGrounded);

		isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGrounded);
		float horizontalInput = Input.GetAxisRaw("Horizontal") * speedBoots;

		if (Input.GetButtonDown("Jump"))
		{
			Jump();
		}

		if (Input.GetButtonDown("Fire1"))
		{
			FireBullets();
		}
		ShowFalling();

		

		if (!isSizeIncreasing)
		{
			if (leftPressed)
			{
				MoveHorizontal(-speedBoots);
			}
			else if (rightPressed)
			{
				MoveHorizontal(speedBoots);
			}
			else if (horizontalInput != 0)
			{
				MoveHorizontal(horizontalInput);
			}
			else if (GameCtrl.instance.checkHarmonyKey() && !isFoundPeople)
			{
				MoveToPeople();
			}
			else if (isFoundPeople)
			{
				anim.SetInteger("State", 4);
			}
			else
			{
				StopMoving();
			}
		}
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
		if (other.gameObject.CompareTag ("Enemy")) {
			GameCtrl.instance.PlayerDiedAnimation(gameObject);
			AudioCtrl.instance.PlayerDied(gameObject.transform.position);
		}
		if (other.gameObject.CompareTag ("ShinningCoin")) {
			GameCtrl.instance.updateCoinCount();
			SFXCtrl.instance.ShowBulletSparkle(other.gameObject.transform.position);
			Destroy(other.gameObject);
			int value = GameCtrl.instance.getItemValue(GameCtrl.Item.ShinningCoin);
            PoinPopup poinPopup = PoinPopup.Create(gameObject.transform.position, value, FloatingTextPrefab, Color.white);
            if (poinPopup != null)
            {
                poinPopup.SetPosition(transform.position + new Vector3(0f, 1f, 0f));
            }
			GameCtrl.instance.updateScore(GameCtrl.Item.ShinningCoin);
			AudioCtrl.instance.CoinPickup(gameObject.transform.position);
		}
		if (other.gameObject.CompareTag ("HarmonyKey")) {
			SFXCtrl.instance.ShowBulletSparkle(other.gameObject.transform.position);
			Destroy(other.gameObject);
			//GameCtrl.instance.updateScore(GameCtrl.Item.HarmonyKey);
			AudioCtrl.instance.CoinPickup(gameObject.transform.position);
			GameCtrl.instance.OpenEnding();
		}

	}

	public void MoveToPeople() {
		// Hitung arah dan jarak antara pemain dan targetPeople.
		Vector3 direction = targetPeople.position - transform.position;
		float distance = direction.magnitude;

		// Normalisasi arah untuk mendapatkan vektor arah.
		Vector3 normalizedDirection = direction / distance;

		// Pemain bergerak ke arah targetPeople.
		rb.velocity = new Vector2(normalizedDirection.x * speed, rb.velocity.y);
		anim.SetInteger("State", 1);

		// Tetapkan arah karakter sesuai dengan perubahan arah
		sr.flipX = normalizedDirection.x < 0;
		Debug.Log("Distance: " + distance);
		
		if (distance < 1.5) {
			isFoundPeople = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.gameObject.tag)
		{
			case "Coin":
				if (SFXOn)
				{
					SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);
				}
				GameCtrl.instance.updateCoinCount();
				GameCtrl.instance.updateScore(GameCtrl.Item.Coin);
				AudioCtrl.instance.CoinPickup(gameObject.transform.position);
				// Destroy(other.gameObject);
				break;
			case "Water":
				SFXCtrl.instance.ShowSplash(other.gameObject.transform.position);
				AudioCtrl.instance.WaterSplash(gameObject.transform.position);
				break;
			case "Powerup_Bullet":
				StartPowerupAnimation(other.gameObject);
				break;
			default:
				break;
		}
	}

	private void StartPowerupAnimation(GameObject powerupObject)
	{
		StartCoroutine(ApplyPowerupAnimation(powerupObject));
	}

	private IEnumerator ApplyPowerupAnimation(GameObject powerupObject)
	{
		isSizeIncreasing = true;

		canFire = true;
		Vector3 powerupPos = powerupObject.transform.position;
		AudioCtrl.instance.PowerUp(gameObject.transform.position);
		Destroy(powerupObject);

		btnFire.SetActive(true);
		if (SFXOn)
		{
			SFXCtrl.instance.ShowBulletSparkle(powerupPos);
		}

		float originalScale = transform.localScale.x;
		float newSizeMultiplier = 1.5f; 
		float animationDuration = 1.2f; 

		float elapsedTime = 0f;

		while (elapsedTime < animationDuration)
		{
			float newSize = Mathf.Lerp(originalScale, originalScale * newSizeMultiplier, elapsedTime / animationDuration);
			sr.color = Color.white;
			anim.SetInteger("State", 0);
			transform.localScale = new Vector3(newSize, newSize, newSize);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.localScale = new Vector3(originalScale, originalScale, originalScale);

		isSizeIncreasing = false;
	}
}