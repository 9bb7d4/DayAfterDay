using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager02 : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Queue<string> sentences;
    public bool isDialogueEnd = false;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public  void StartDialogue(Dialogue02 dialogue)
    {   
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence) 
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue() 
    {
        Debug.Log("End of conversation");
        isDialogueEnd = true;
    }
}

