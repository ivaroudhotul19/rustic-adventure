using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCtrl3 : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text spikyfoetext;
    private Text ratroguetext;

   private void Awake()
    {
        spikyfoetext = transform.Find("message").Find("spikyfoetext").GetComponent<Text>();
        ratroguetext = transform.Find("message2").Find("ratroguetext").GetComponent<Text>();
    }

    private void Start() {
        textWriter.AddWriter(spikyfoetext, "Bunuh musuh Spikyfoe dengan peluru. Musuh ini memiliki serangan sebesar 20 poin dan memiliki 25 poin nyawa", .1f, true);
        textWriter.AddWriter(ratroguetext, "Bunuh musuh Mushmaw dengan peluru. Musuh ini memiliki serangan sebesar 10 poin dan memiliki 20 poin nyawa", .1f, true);
    }  
}
