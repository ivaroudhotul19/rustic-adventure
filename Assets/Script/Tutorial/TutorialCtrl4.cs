using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCtrl4 : MonoBehaviour
{
   [SerializeField] private TextWriter textWriter;
    private Text dinotext;
    private Text fishtext;
    private Text firetext;

   private void Awake()
    {
        dinotext = transform.Find("message").Find("dinotext").GetComponent<Text>();
        fishtext = transform.Find("message2").Find("fishtext").GetComponent<Text>();
        firetext = transform.Find("message3").Find("firetext").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(dinotext, "Habisi musuh bos ini dengan tembakan, tapi ingat, setiap serangannya bisa mengurangi nyawamu sebanyak 2", .1f, true);
        textWriter.AddWriter(fishtext, "Jangan kena ikan itu supaya kamu nggak mati", .1f, true);
        textWriter.AddWriter(firetext, "Hindarilah perangkap api ini agar nyawamu tidak berkurang", .1f, true);
    }  
}
