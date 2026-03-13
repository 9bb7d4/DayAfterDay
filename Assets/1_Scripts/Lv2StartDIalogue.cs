using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class Lv2StartDIalogue : MonoBehaviour
{
    public GameObject m_BoxImage;
    public TMP_Text m_dialogue;
    public GameObject m_AllBoxs;
    private bool isBoxShowed = false;


    private void Update()
    {
        if (isBoxShowed==true && Input.GetKeyDown(KeyCode.Tab)) 
        {
            m_BoxImage.SetActive(false);
            isBoxShowed = false;
            Time.timeScale = 1f;
            Destroy(m_AllBoxs);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            Time.timeScale = 0f;
            m_BoxImage.SetActive(true);
            isBoxShowed = true;
        }
    }
}