using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletCtrl : MonoBehaviour {
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
		if(other.gameObject.CompareTag("Player")) {
			GameCtrl.instance.ReducePlayerHealthDino();
			Destroy(gameObject);
		}
	}
}
