using UnityEngine;

public class DroppingPlatform : MonoBehaviour {
    public float droppingDelay;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Fungsi yang dipanggil ketika objek lain bersentuhan dengan platform
        if(other.gameObject.CompareTag("PlayerFeet")) {
            // suatu cara untuk menunda pemanggilan fungsi dalam Unity untuk jangka waktu tertentu
            Invoke("StartDropping", droppingDelay);
        }
	}

    void StartDropping() {
        rb.isKinematic = false;
    }
}
