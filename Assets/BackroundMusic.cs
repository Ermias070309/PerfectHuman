using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip backgroundMusic; // Reference to the music clip

    void Start()
    {
        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic; // Assign the music clip
            audioSource.loop = true; // Enable looping
            audioSource.volume = 0.5f; // Set volume
            audioSource.Play(); // Start playing the music
        }
        else
        {
            Debug.LogWarning("AudioSource or BackgroundMusic is not assigned!");
        }
    }

    void OnDisable()
    {
        // Stop the music when the scene changes
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
