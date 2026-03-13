using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private bool isLKeyPressed = false;
    private float lKeyTimer = 0f;
    private float requiredHoldTime = 3f;

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (!isLKeyPressed)
            {
                isLKeyPressed = true;
            }

            lKeyTimer += Time.deltaTime;

            if (lKeyTimer >= requiredHoldTime)
            {
                SwitchToNextScene();
            }
        }
        else
        {
            isLKeyPressed = false;
            lKeyTimer = 0f;
        }
    }

    void SwitchToNextScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}