using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement; // 需要引入場景管理器

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 2.0f;
    private float currentTime;
    public Text countdownText;
    public string nextSceneName; // 下一個場景的名稱

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            currentTime = 0;
        }

        UpdateCountdownText();

        if (currentTime == 0)
        {
            // 載入下一個場景
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void UpdateCountdownText()
    {
        countdownText.text = "Time: " + currentTime.ToString("F0");
    }
}
