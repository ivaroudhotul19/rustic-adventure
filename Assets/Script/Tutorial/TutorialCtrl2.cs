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
        textWriter.AddWriter(cointext, "Ambil koin yang berkilau ini saat bermain, koin ini akan meningkatkan skormu", .1f, true);
        textWriter.AddWriter(mushmawtext, "Untuk mengalahkan musuh ini, cukup tembak dia. Tapi hati-hati, setiap serangan dari musuh ini bisa mengurangi nyawamu sebesar seperempat", .1f, true);
    }  
}
