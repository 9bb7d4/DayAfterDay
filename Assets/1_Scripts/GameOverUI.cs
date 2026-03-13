using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{


    public void SetActive(bool value)
    {
        Debug.Log("SetActive: " + value);
        SetMouseCursorVisible(true);
        gameObject.SetActive(value);
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    public void SetMouseCursorVisible(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        if (Time.timeScale == 0f)
        {
            SceneManager.LoadScene(0);

            Time.timeScale = 1f;
            
        }

    

    }

}
