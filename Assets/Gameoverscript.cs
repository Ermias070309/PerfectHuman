using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameoverscript : MonoBehaviour
{
    public void RestartButton()
    {
        // Log a message to the console to confirm the method is called
        Debug.Log("RestartButton clicked");

        // Load the scene named "Scene1"
        SceneManager.LoadScene("Scene 1");
    }
}
