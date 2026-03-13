using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class StoryMarquee : MonoBehaviour
{
    public TMP_Text marqueeText;
    public float scrollSpeed;
    public float delayBeforeDestroy = 30f;  // 延遲刪除時間，單位為秒

    private RectTransform marqueeRect;

    void Start()
    {
        marqueeRect = marqueeText.GetComponent<RectTransform>();
        StartCoroutine(ScrollText());
        StartCoroutine(DestroyAfterDelay(delayBeforeDestroy));
    }

    IEnumerator ScrollText()
    {
        while (true)
        {
            if (Time.timeScale != 0) // 檢查時間是否暫停
            {
                marqueeRect.localPosition += new Vector3(0f, scrollSpeed * Time.unscaledDeltaTime, 0f);
                if (marqueeRect.localPosition.y >= marqueeText.preferredHeight)
                {
                    marqueeRect.localPosition -= new Vector3(0f, marqueeText.preferredHeight, 0f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Return)) // 檢測是否按下 Enter 鍵
            {
                SceneManager.LoadScene(0); // 載入編號0場景
            }
            yield return null;
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0); // 載入編號0場景
    }
}