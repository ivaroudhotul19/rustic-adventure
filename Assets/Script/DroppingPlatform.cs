using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    public float droppingDelay;

    private Vector2 initialPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Fungsi yang dipanggil ketika objek lain bersentuhan dengan platform
        if (other.gameObject.CompareTag("PlayerFeet"))
        {
            // Suatu cara untuk menunda pemanggilan fungsi dalam Unity untuk jangka waktu tertentu
            Invoke("StartDropping", droppingDelay);
        }
    }

    void StartDropping()
    {
        rb.isKinematic = false;
        // Menunda pemanggilan metode untuk mengembalikan posisi setelah jatuh
        Invoke("ResetPosition", 5f);
    }

    void ResetPosition()
    {
        // Mengembalikan posisi platform ke posisi awal
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        transform.position = initialPosition;
    }
}
