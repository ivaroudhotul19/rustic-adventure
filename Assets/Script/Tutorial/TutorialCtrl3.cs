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
        textWriter.AddWriter(spikyfoetext, "Tembak musuh ini, tapi perhatikan, setiap serangan bisa mengurangi nyawamu sebanyak 1", .1f, true);
        textWriter.AddWriter(ratroguetext, "Tembak musuh ini, tapi hati-hati, setiap serangannya bisa ngurangin nyawamu setengah", .1f, true);
    }  
}
