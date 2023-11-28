using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCtrl5 : MonoBehaviour
{
   [SerializeField] private TextWriter textWriter;
    private Text cratetext;
    private Text kuncitext;
    //private Text firetext;

   private void Awake()
    {
        cratetext = transform.Find("message").Find("cratetext").GetComponent<Text>();
        kuncitext = transform.Find("message2").Find("kuncitext").GetComponent<Text>();
       // firetext = transform.Find("message3").Find("firetext").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(cratetext, "Ada beberapa peti yang bisa dibuka", .1f, true);
        textWriter.AddWriter(kuncitext, "Ambil kunci ini untuk menyelesaikan level dan melangkah ke level berikutnya", .1f, true);
       // textWriter.AddWriter(firetext, "Hindarilah perangkap api ini agar nyawamu tidak berkurang", .1f, true);
    }  
}
