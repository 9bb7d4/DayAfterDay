using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Lv3Dialogue : MonoBehaviour
{
    public Dialogue02 dialogue;
    public GameObject m_Roger;
    public GameObject m_YuanP;
    public GameObject m_dialogue;
    private bool startConversation = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Roger.SetActive(true);
        m_YuanP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<BossDisappear>().bossReplaced == true)
        {
            startConversation = true;
            Invoke("StartConversation", 2f);
            if (startConversation == true)
            {
                if (FindObjectOfType<DialogueManager02>().sentences.Count == 5)
                {
                    Debug.Log(FindObjectOfType<DialogueManager02>().sentences.Count);
                    m_Roger.SetActive(false);
                    m_YuanP.SetActive(true);
                }
                else if (FindObjectOfType<DialogueManager02>().sentences.Count == 3)
                {
                    Debug.Log(FindObjectOfType<DialogueManager02>().sentences.Count);
                    m_Roger.SetActive(false);
                    m_YuanP.SetActive(true);
                }
                else 
                {
                    m_Roger.SetActive(true);
                    m_YuanP.SetActive(false);
                }
                if (FindObjectOfType<DialogueManager02>().isDialogueEnd == true)
                {
                    SceneManager.LoadScene(5);
                    Time.timeScale = 1f;
                }
            }
        }
    }

    void StartConversation()
    {
        m_dialogue.SetActive(true);
        if (startConversation == true)
        {
            FindObjectOfType<DialogueManager02>().StartDialogue(dialogue);
            Time.timeScale = 0f;
            Debug.Log(FindObjectOfType<DialogueManager01>().sentences.Count);
        }

    }
 }
