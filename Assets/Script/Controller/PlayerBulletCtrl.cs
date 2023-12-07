using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCtrl : MonoBehaviour {
	public Vector2 velocity;
	private SimpleMovement simpleMovement;

	Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Enemy")) {
			SimpleMovement simpleMovement = other.gameObject.GetComponent<SimpleMovement>();
            if (simpleMovement != null)
            {
                simpleMovement.TakeDamage(1);
            }
			SimpleMovementTikus simpleMovementTikus = other.gameObject.GetComponent<SimpleMovementTikus>();
			if (simpleMovementTikus != null)
			{
				simpleMovementTikus.TakeDamage(1);
			}
			SimpleMovementLandak simpleMovementLandak = other.gameObject.GetComponent<SimpleMovementLandak>();
			if (simpleMovementLandak != null)
			{
				simpleMovementLandak.TakeDamage(1);
			}
            Destroy(gameObject);
		} else if(!other.gameObject.CompareTag("Player")) {
			Destroy(gameObject);
		}
	}
}
