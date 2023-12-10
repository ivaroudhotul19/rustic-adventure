using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UI {
    public Text txtCoinCount;
    public Text txtScore;
    public Text textTimer;
    public Image[] heartImages; // Array untuk gambar hati
    public Sprite heartFull; // Sprite hati penuh
    public Sprite heartThreeQuarter; // Sprite tiga perempat hati
    public Sprite heartHalf; // Sprite setengah hati
    public Sprite heartQuarter; // Sprite seperempat hati
    public Sprite heartEmpty; // Sprite hati kosong
    public Image keyImage;
    public Sprite keySprite;

    public GameObject panelGameOver;
    public GameObject panelPause;
}
