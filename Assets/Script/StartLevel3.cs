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
        textWriter.AddWriter(text, "Setelah lewat Hutan Perdamaian, Ruvy masuk ke Level Kedua di lembah misterius. Di sini, dia menghadapi tantangan baru, yaitu landak berduri yang bisa menyakitkan. Selain itu, masih ada musuh lama seperti tikus dan jamur beracun. Ruvy bisa menembak setelah makan ceri, dan kalau berhasil menembak musuh, pemain dapat skor ekstra. Pemain juga harus cari kunci di tengah jalan untuk buka pintu ke level berikutnya. Jadi, permainannya semakin seru dan menantang, di mana pemain harus hindari musuh, kumpulkan kunci, dan berusaha dapat skor tinggi.", .1f, true);
    }  
}
