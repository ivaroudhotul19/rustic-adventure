using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCtrl : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text messageText;
    private Text ruvytext;
    private void Awake()
    {
        messageText = transform.Find("message").Find("messageText").GetComponent<Text>();
        ruvytext = transform.Find("message2").Find("ruvytext").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(messageText, "Ambil ceri ini, dan kamu akan memiliki kemampuan untuk menembak musuh", .1f, true);
        textWriter.AddWriter(ruvytext, "Ruvy, rubah berani yang mengendalikan permainan dengan lima nyawa. Dengan kecerdikan dan ketangkasan, ia membimbing pemain melewati tantangan, siap menghadapi setiap ujian petualangan", .1f, true);
    }  
}

