using UnityEngine;
using UnityEngine.UI;

public class StartLevel3 : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text text;
    private void Awake() {
        text = transform.Find("message").Find("text").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(text, "Setelah melewati tantangan di Hutan Perdamaian dan berhasil menaklukkan landak berduri serta musuh-musuh lainnya, Ruvy melangkah ke Level Ketiga yang tidak kalah menantang, Pegunungan Harmony. Dalam petualangan ini, Ruvy harus mengambil kunci ditengah perjalanan untuk membuka pintu level complete, pemain akan dihadapkan pada musuh terkuat, Dino. Ruvy harus menghadapi Dino yang menyimpan kunci harmoni untuk dapat menyelesaikan permainan ini.", .1f, true);
    }  
}
