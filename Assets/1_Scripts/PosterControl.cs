using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PosterControl : MonoBehaviour
{
    public GameObject m_PosterImage;
    public TMP_Text m_PosterHint;
    private bool isPosterShowed = false;
    private bool isInPosterArea = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInPosterArea == true && isPosterShowed == false && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("open");
            m_PosterHint.gameObject.SetActive(false);
            m_PosterImage.SetActive(true);
            isPosterShowed = true;
        }
        else if (isInPosterArea == true && isPosterShowed == true && Input.GetKeyDown(KeyCode.Tab))
        {
            m_PosterImage.SetActive(false);
            isPosterShowed = false;
            m_PosterHint.gameObject.SetActive(false);
        }
        else if (isInPosterArea == false && isPosterShowed == true && Input.GetKeyDown(KeyCode.Tab))
        {
            m_PosterImage.SetActive(false);
            isPosterShowed = false;
            m_PosterHint.gameObject.SetActive(false);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_PosterHint.gameObject.SetActive(true);
            isInPosterArea = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInPosterArea = false;
            m_PosterHint.gameObject.SetActive(false);
        }
    }

}
