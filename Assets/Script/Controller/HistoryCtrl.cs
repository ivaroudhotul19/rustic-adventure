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
        textWriter.AddWriter(text, "Ruvy, rubah oranye dengan bulu lebat, menjalani petualangan untuk mengembalikan keseimbangan alam di desa yang terancam oleh para monster. Pemain mengikuti Ruvy melalui Hutan Perdamaian, menghindari musuh jamur dan tikus sambil mengumpulkan kunci permainan. Setelah hutan, Ruvy melewati lembah misterius dengan rintangan yang lebih rumit dan musuh lebih kuat, seperti landak berduri. Perjalanan mencapai puncak di Pegunungan Harmony, di mana Ruvy menghadapi musuh terkuat, Dino. Pemain membantu Ruvy mengumpulkan Coin Harmony dan melawan Dino untuk mendapatkan Kunci Harmony, mengakhiri petualangan epik ini.", .1f, true);
    }  
}
