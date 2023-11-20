using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform player;
    public float yOffset;
    public float minX, minY;
    
    // Untuk batasan atas
    public float maxY;

    // Gunakan LateUpdate untuk memastikan kamera bergerak setelah pemain pindah
    void LateUpdate()
    {
        // Mendapatkan posisi pemain
        float targetX = player.position.x;
        float targetY = player.position.y + yOffset;

        // Membatasi posisi kamera agar tidak melewati batasan kiri, atas, dan bawah
        targetX = Mathf.Max(targetX, minX);
        targetY = Mathf.Clamp(targetY, minY, maxY);

        // Menetapkan posisi kamera
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
