using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public TMP_Text m_pullHint;
    private bool canLoadNextScene = false;

    private void Update()
    {
        if (canLoadNextScene == true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            canLoadNextScene = true;
            m_pullHint.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            canLoadNextScene = false;
            m_pullHint.gameObject.SetActive(false);
        }
    }
}