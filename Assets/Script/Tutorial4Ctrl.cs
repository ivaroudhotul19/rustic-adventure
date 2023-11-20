using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial4Ctrl : MonoBehaviour
{
   [SerializeField] private TextWriter textWriter;
    private Text dinotext;

   private void Awake()
    {
        dinotext = transform.Find("message").Find("dinotext").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(dinotext, "Bunuh musuh Dino dengan peluru. Musuh ini memiliki serangan sebesar 50 poin dan memiliki 100 poin nyawa", .1f, true);
    }  
}
