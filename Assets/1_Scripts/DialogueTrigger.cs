using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue01 dialogue;

    public void TriggerDialogue() 
    {
        FindObjectOfType<DialogueManager01>().StartDialogue(dialogue);
    }

    
}
