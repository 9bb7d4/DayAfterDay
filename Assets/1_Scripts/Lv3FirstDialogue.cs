using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Lv3FirstDialogue : MonoBehaviour
{
    public Dialogue01 dialogue;
    public GameObject m_Roger;
    public GameObject m_YuanP;
    public GameObject m_dialogue;
    public GameObject m_dialogueManager01;

    //public GameObject m_LastDialogue;

    private bool startConversation = false;

    private bool isFirstDialogueShowed = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Roger.SetActive(true);
        m_YuanP.SetActive(false);
        //m_LastDialogue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstDialogueShowed == true)
        {
            if (FindObjectOfType<DialogueManager01>().sentences.Count % 2 == 0&& FindObjectOfType<DialogueManager01>().sentences.Count!=0)
            {
                Debug.Log(FindObjectOfType<DialogueManager01>().sentences.Count);
                m_Roger.SetActive(false);
                m_YuanP.SetActive(true);
            }
            else
            {
                m_Roger.SetActive(true);
                m_YuanP.SetActive(false);
            }
            if (FindObjectOfType<DialogueManager01>().isDialogueEnd == true)
            {
                Time.timeScale = 1f;
                m_dialogue.SetActive(false);
                m_dialogueManager01.SetActive(false);
                //m_LastDialogue.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            startConversation = true;
            m_dialogue.SetActive(true);
            if (startConversation == true)
            {
                FindObjectOfType<DialogueManager01>().StartDialogue(dialogue);
                Time.timeScale = 0f;
                isFirstDialogueShowed=true;
                Debug.Log(FindObjectOfType<DialogueManager01>().sentences.Count);
            }
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        if (isFirstDialogueShowed == true)
    //        {
    //            if (FindObjectOfType<DialogueManager>().sentences.Count %2== 0)
    //            {
    //                Debug.Log(FindObjectOfType<DialogueManager>().sentences.Count);
    //                m_Roger.SetActive(false);
    //                m_YuanP.SetActive(true);
    //            }
    //            //else if (FindObjectOfType<DialogueManager>().sentences.Count == 2)
    //            //{
    //            //    Debug.Log(FindObjectOfType<DialogueManager>().sentences.Count);
    //            //    m_Roger.SetActive(false);
    //            //    m_YuanP.SetActive(true);
    //            //}
    //            //else if (FindObjectOfType<DialogueManager>().sentences.Count == 0)
    //            //{
    //            //    Debug.Log(FindObjectOfType<DialogueManager>().sentences.Count);
    //            //    m_Roger.SetActive(false);
    //            //    m_YuanP.SetActive(true);
    //            //}
    //            else
    //            {
    //                m_Roger.SetActive(true);
    //                m_YuanP.SetActive(false);
    //            }
    //            if (FindObjectOfType<DialogueManager>().isDialogueEnd == true)
    //            {
    //                Time.timeScale = 1f;
    //                Destroy(m_dialogue);
    //            }
    //        }
        
    //    }
    //}

    //private void OnCollisionExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        startConversation = false;
    //        Destroy(this.gameObject);
    //    }
    //}
}
