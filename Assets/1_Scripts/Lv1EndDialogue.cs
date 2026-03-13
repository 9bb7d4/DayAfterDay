using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Lv1EndDialogue : MonoBehaviour
{
    public GameObject m_BoxImage;
    public string[] dialogue;
    public TMP_Text m_dialogue;

    private bool isBoxShowed = false;

    private int a = 0;

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (isBoxShowed == true && Input.GetKeyDown(KeyCode.Tab))
            {
                if (a > 1)
                {
                    m_BoxImage.SetActive(false);
                    Time.timeScale = 1f;
                    break;
                }

                m_dialogue.text = dialogue[a];
                a++;
                return;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            Time.timeScale = 0f;
            isBoxShowed = true;
            m_BoxImage.SetActive(true);
        }
    }
}