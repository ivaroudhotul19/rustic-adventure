using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerSetting : MonoBehaviour
{
    [SerializeField]
    private float timerDuration;

    private float timer; // Timer yang berjalan
    public string sceneName;     

    void Start()
    {
        // Mulai timer ketika scene dimuat
        timer = timerDuration;
    }

    void Update()
    {
        // Kurangi timer setiap frame
        timer -= Time.deltaTime;

        // Cek apakah timer sudah habis
        if (timer <= 0f)
        {
            // Pindah ke scene tujuan
            LoadTargetScene();
        }
    }

    void LoadTargetScene()
    {
        // Menggunakan SceneManager untuk memuat scene berdasarkan namanya
        SceneManager.LoadScene(sceneName);
    }
}
