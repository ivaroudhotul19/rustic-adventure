using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCtrl : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text messageText;
    private void Awake()
    {
        messageText = transform.Find("message").Find("messageText").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(messageText, "Ambil ceri ini untuk mengisi persediaan peluru. Ceri ini adalah power-up yang memungkinkan karakter menembakkan peluru ke musuh", .1f, true);

    }  
}

