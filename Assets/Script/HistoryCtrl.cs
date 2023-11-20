using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryCtrl : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text text;
    private void Awake() {
        text = transform.Find("message").Find("text").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(text, "Dalam dunia yang indah, pemain memulai petualangan sebagai Rustic, seekor rubah oranye dengan bulu yang lebat, yang tinggal di desa yang mengalami ketidakseimbangan alam akibat ulah para monster. Pemain merasa panggilan alam untuk memulihkan keseimbangan dan memulai perjalanan mencari Kunci Harmoni di hutan misterius. Di Level 1, Rustic harus menghindari musuh seperti Mushmaw dan Rat rogue sambil mengumpulkan Coin Harmony. Level 2 membawa Rustic ke lembah misterius dengan rintangan dan musuh yang lebih kuat, termasuk Spikyfoe. Akhirnya, Rustic tiba di Pegunungan Harmony dalam Level 3, menghadapi musuh terkuat, Dino, untuk mendapatkan Kunci Harmony. Pesan cerita ini mengingatkan pemain tentang pentingnya menjaga keseimbangan alam dan lingkungan.", .1f, true);
    }  
}
