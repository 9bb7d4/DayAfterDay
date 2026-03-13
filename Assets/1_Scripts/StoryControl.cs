using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Globalization;

public class StoryControl : MonoBehaviour
{

    public GameObject[] m_text;
    public GameObject m_text01;
    public GameObject m_text02;
    public GameObject m_text03;
    int a = 0;

    public GameObject TextGroup;
    public bool isTextGroupShowed =true;

    public GameObject m_dialogBox;
    public GameObject m_Character;
    public Dialogue01 dialogue;

    AudioSource m_audioSource;


    // Start is called before the first frame update
    void Start()
    {
        m_text01.SetActive(false);
        Invoke("StoryBegin",1.0f);

        m_text = new GameObject[2];
        m_text[0] = m_text02;
        m_text[1] = m_text03;
        m_text[0].SetActive(false);
        m_text[1].SetActive(false);

        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++) 
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                if (a >= 2)
                {
                    TextGroup.SetActive(false);
                    isTextGroupShowed = false; 
                    break;
                }
                m_text[a].SetActive(true);
                a++;
                return;
            }
        }
        if (isTextGroupShowed == false) 
        {
            m_dialogBox.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Start conversation.");
                FindObjectOfType<DialogueManager01>().StartDialogue(dialogue);
            }
            else if (FindObjectOfType<DialogueManager01>().isDialogueEnd == true)
            {
                SceneManager.LoadScene(2);
            }
            if (FindObjectOfType<DialogueManager01>().sentences.Count == 0 )
            {             
                m_Character.SetActive(false);
            }
            else if (FindObjectOfType<DialogueManager01>().sentences.Count == 2)
            {
                m_Character.SetActive(false);
            }
            else if (FindObjectOfType<DialogueManager01>().sentences.Count == 5)
            {
                m_Character.SetActive(false);
            }
            else 
            {
                m_Character.SetActive(true);
            }
        }
    }

    void StoryBegin() 
    {
        m_text01.SetActive(true);
    }
}
