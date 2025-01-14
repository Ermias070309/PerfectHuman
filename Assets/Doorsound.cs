using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorInteraction2D : MonoBehaviour
{
    // Reference to the AudioSource component
    public AudioSource audioSource;

    // The sound effect to play when the door opens
    public AudioClip doorOpenSound;

    // Is the door currently locked or unlocked
    public bool isLocked = true;

    // Interaction radius
    public float interactionRadius = 2f;

    // Reference to the player
    public Transform player;

    void Update()
    {
        // Check if the player presses E and is within interaction range
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerClose())
        {
            InteractWithDoor();
        }
    }

    void InteractWithDoor()
    {
        if (isLocked)
        {
            Debug.Log("The door is locked!");
            // Optional: Play a "locked" sound effect
        }
        else
        {
            Debug.Log("The door is unlocked and opens!");
            PlayDoorSound();
        }
    }

    void PlayDoorSound()
    {
        // Play the sound effect
        if (audioSource != null && doorOpenSound != null)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or doorOpenSound is missing!");
        }
    }

    bool IsPlayerClose()
    {
        return Vector2.Distance(transform.position, player.position) <= interactionRadius;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the interaction radius in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

