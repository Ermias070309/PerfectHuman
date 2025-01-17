using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector3 defaultSpawnPosition = new Vector3(0, 0, 0); 
    private Vector3 targetSpawnPosition;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        targetSpawnPosition = defaultSpawnPosition; 
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    public void SetSpawnPosition(Vector3 newPosition)
    {
        targetSpawnPosition = newPosition; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = targetSpawnPosition; 
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }
}