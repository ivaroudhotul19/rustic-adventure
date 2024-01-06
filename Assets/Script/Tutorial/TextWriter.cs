using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextWriter : MonoBehaviour
{
    private List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        textWriterSingleList.Add(new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters));
    }

    private void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            textWriterSingleList[i].Update();
        }
    }

    public class TextWriterSingle
    {
        private Text uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;
        private string visibleText;

        public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            characterIndex = 0;
            visibleText = "";
            timer = timePerCharacter;
        }

        public void Update()
        {
            if (uiText != null)
            {
                timer -= Time.deltaTime;
                while (timer <= 0f ) {
                    timer += timePerCharacter;

                    if (invisibleCharacters) {
                        visibleText += textToWrite[characterIndex];
                    }
                    else {
                        visibleText += "<color=#00000000>" + textToWrite[characterIndex] + "</color>";
                    }
                    uiText.text = visibleText;
                    characterIndex++;
                }

                if (characterIndex >= textToWrite.Length) {
                    uiText = null;
                    return;
                }
            }
        }
    }
}