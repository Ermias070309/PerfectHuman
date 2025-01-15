using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementWithSounds : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Sound Settings")]
    public AudioSource audioSource;    // AudioSource component for playing sounds
    public AudioClip walkingSound;     // Sound effect for walking
    public AudioClip jumpingSound;     // Sound effect for jumping

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isWalking = false;    // Track if walking sound is currently playing

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Check for AudioSource assignment
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
    }

    private void HandleMovement()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the player
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Play or stop walking sound based on movement
        if (Mathf.Abs(horizontalInput) > 0.1f) // Ensure walking sound plays even during a jump
        {
            if (!isWalking)
            {
                PlayWalkingSound();
            }
        }
        else
        {
            StopWalkingSound();
        }
    }

    private void HandleJumping()
    {
        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            PlayJumpingSound();
        }
    }

    private void PlayWalkingSound()
    {
        if (audioSource != null && walkingSound != null)
        {
            audioSource.clip = walkingSound;
            audioSource.loop = true;
            audioSource.Play();
            isWalking = true;
        }
        else
        {
            Debug.LogWarning("Walking sound or AudioSource is not assigned!");
        }
    }

    private void StopWalkingSound()
    {
        if (audioSource != null && isWalking)
        {
            audioSource.Stop();
            isWalking = false;
        }
    }

    private void PlayJumpingSound()
    {
        if (audioSource != null && jumpingSound != null)
        {
            audioSource.PlayOneShot(jumpingSound);
        }
        else
        {
            Debug.LogWarning("Jumping sound or AudioSource is not assigned!");
        }
    }

    // Simple ground check using collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}


