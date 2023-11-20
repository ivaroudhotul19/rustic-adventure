using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCtrl2 : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text cointext;
    private Text mushmawtext;

   private void Awake()
    {
        cointext = transform.Find("message").Find("cointext").GetComponent<Text>();
        mushmawtext = transform.Find("message2").Find("mushmawtext").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(cointext, "Ambil koin ini untuk meningkatkan skor permainan", .1f, true);
        textWriter.AddWriter(mushmawtext, "Bunuh musuh Mushmaw dengan peluru. Musuh ini memiliki serangan sebesar 5 poin dan memiliki 10 poin nyawa", .1f, true);
    }  
}
