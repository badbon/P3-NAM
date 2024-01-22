using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class UpdateDialogueText : MonoBehaviour
{
    public TMP_Text textObj;

    public float typingDialogueDelay = 0.05f;

    void Start()
    {
        textObj = GetComponent<TMP_Text>();
        if(textObj == null)
        {
            Debug.Log("Missing text var for dialogue!");
        }

        StartCoroutine(DelayUpdateTypingText("Tari tari"));
    }

    void Update()
    {
        
    }

    // Print dialogue letter by letter, with delay
    public bool PushTypingText(String dialogueText)
    {
        char[] letters = dialogueText.ToCharArray();
        char nextLetter = '#';

        for(int i = 0; i > letters.Count(); i++)
        {
            nextLetter = letters[i];
            Debug.Log(nextLetter);
        }

        String currentText = textObj.text;
        String newText = textObj.text + nextLetter;
        return true; // Completed successfully
    }

    public IEnumerator DelayUpdateTypingText(String dialogueText)
    {
        char[] letters = dialogueText.ToCharArray();
        char nextLetter = '#';
        textObj.text = " "; // Clear last text, typing new dialogue

        for (int i = 0; i < letters.Count(); i++)
        {
            nextLetter = letters[i];
            Debug.Log(nextLetter);

            textObj.text += nextLetter;

            yield return new WaitForSeconds(typingDialogueDelay);
        }


        yield return null; 
    }
}
