using UnityEngine.SceneManagement;
using UnityEngine;


public class LoadSceneOnDisable : MonoBehaviour
{

    private void OnDisable()
    {   
        SceneManager.LoadScene(3);
    }
    
}
    