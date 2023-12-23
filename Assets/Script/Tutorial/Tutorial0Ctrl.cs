using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial0Ctrl : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text btnAttack;
    private Text btnMovetext;
    private Text btnJump;
    private void Awake()
    {
        btnAttack = transform.Find("message").Find("btnAttack").GetComponent<Text>();
        btnMovetext = transform.Find("message2").Find("btnMovetext").GetComponent<Text>();
        btnJump = transform.Find("message3").Find("btnJump").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(btnAttack, "Button ini dapat berfungsi jika sudah mengambil ceri. ", .1f, true);
        textWriter.AddWriter(btnMovetext, "Button ini digunakan untuk menggerakkan pemain ke arah kanan atau kiri", .1f, true);
        textWriter.AddWriter(btnJump, "Tombol ini digunakan untuk melompat, bisa digunakan untuk double jump", .1f, true);
    }  
}
