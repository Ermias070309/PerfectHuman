using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startmenucode : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void OnexitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}


