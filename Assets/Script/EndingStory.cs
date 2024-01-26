using UnityEngine;
using UnityEngine.UI;

public class EndingStory : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text text;
    private void Awake() {
        text = transform.Find("message").Find("text").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(text, "Dengan mengumpulkan Coin Harmony dan menghadapi Dino, Ruvy meraih kemenangan di Pegunungan Harmony, mengakhiri perjalanan dengan gemilang. Perjalanan ini mengajarkan bahwa tantangan di dunia fantasi bukan hanya tentang mengalahkan musuh, tapi juga menemukan kekuatan dalam diri sendiri. Pemain diberi pengalaman berharga bahwa setiap rintangan dapat diatasi dengan tekad dan semangat pantang menyerah. Selamat atas kemenanganmu, Pahlawan Harmony!", .1f, true);
    }  
}
