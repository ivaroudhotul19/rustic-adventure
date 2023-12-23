using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevel1 : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text text;
    private void Awake() {
        text = transform.Find("message").Find("text").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(text, "Ruvy masuk ke Level Pertama di Hutan Perdamaian yang subur, dihadang oleh musuh jamur beracun dan tikus hutan. Pemain harus mengendalikan Ruvy, menghindari jebakan ikan dan api, sambil mencari kunci di tengah perjalanan untuk melanjutkan ke level berikutnya. Untuk mengalahkan musuh, Ruvy perlu mengambil ceri yang memberinya kekuatan, dan pemain akan mendapat skor tambahan setiap kali berhasil mengalahkan musuh. Perjalanan  ini mengharuskan pemain untuk cepat tanggap dan cerdas dalam melewati rintangan, mencapai tujuan, dan meraih skor tertinggi.", .1f, true);
    }  
}
