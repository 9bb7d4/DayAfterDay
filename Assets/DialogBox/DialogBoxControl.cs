using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxControl : MonoBehaviour
{
    public GameObject DestroyedObject;
    public GameObject[] DialogImage;
    //public Text dialogBoxText;
    //public string signText;

    public float displayTime = 2.0f; // 設定圖像出現後持續顯示的時間

    void Start()
    {
        for (int i = 0; i < DialogImage.Length; i++)
        {
            DialogImage[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            //dialogBoxText.text = signText;

            DialogImage[0].SetActive(true);

            StartCoroutine(HideDialogImage());
        }
    }

    IEnumerator HideDialogImage()
    {
        yield return new WaitForSeconds(displayTime);

        for (int i = 0; i < DialogImage.Length; i++)
        {
            DialogImage[i].SetActive(false);
        }

        gameObject.SetActive(false);
        Destroy(DestroyedObject, 2);
    }
}
