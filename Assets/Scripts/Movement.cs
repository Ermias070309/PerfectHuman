using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWithSounds : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public bool isGrounded;
    bool swordnearby = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Sound Settings")]
    public AudioSource audioSource; // AudioSource component for playing sounds
    public AudioClip walkingSound;  // Sound effect for walking
    public AudioClip jumpingSound;  // Sound effect for jumping
    public AudioClip swordSwingSound; // Sound effect for sword swinging
    GameObject swordonground;
    private bool isWalking = false; // Track if walking sound is currently playing

    [Header("Sword Settings")]
    public GameObject swordInHand; // Sword GameObject in the player's hand
    public bool hasSword = false;  // Whether the player has picked up the sword
    public AudioClip swordPickupSound; // Sound effect for picking up the sword

    private bool isSwordAttack = false; // Track sword attack state
    private BoxCollider2D swordCollider; // The sword's collider

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Check for AudioSource assignment
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }

        // Ensure the sword starts inactive
        if (swordInHand != null)
        {
            swordInHand.SetActive(false);
            swordCollider = swordInHand.GetComponent<BoxCollider2D>();
            if (swordCollider != null)
            {
                swordCollider.enabled = false; // Disable sword collider initially
            }
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleSwordActions();
        UpdateAnimatorParameters();

        // Pick up the sword when pressing 'E' near the sword
        if (Input.GetKeyDown(KeyCode.E) && swordnearby)
        {
            PickUpSword();
            Destroy(swordonground); // Destroy the sword object on the ground
        }
    }

    private void HandleMovement()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0;
        }

        // Handle walking sound
        if (Mathf.Abs(horizontalInput) > 0.1f)
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
            animator.SetTrigger("Jump"); // Trigger jump animation
        }
    }

    private void HandleSwordActions()
    {
        if (hasSword)
        {
            animator.SetBool("HoldingSword", true); // Update Animator that player has sword

            // Sword attack on pressing 'E' (Only if sword is in hand)
            if (Input.GetKeyDown(KeyCode.E) && !isSwordAttack)
            {
                SwordAttack();
               
            }
        }
        
    }

    private void SwordAttack()
    {
        if (!isSwordAttack)
        {
            isSwordAttack = true;
            animator.SetTrigger("fightingswordplayer"); // Correct animator trigger name

            // Play sword swing sound
            if (audioSource != null && swordSwingSound != null)
            {
                audioSource.PlayOneShot(swordSwingSound);
            }

            // Enable sword collider during attack
            if (swordCollider != null)
            {
                swordCollider.enabled = true;
            }

            // Reset sword attack state after animation (use an animation event or delay)
            Invoke(nameof(ResetSwordAttack), 0.5f); // Adjust delay as per your sword attack animation length
        }
    }

    private void ResetSwordAttack()
    {
        isSwordAttack = false;

        // Disable sword collider after attack animation ends
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }

    private void UpdateAnimatorParameters()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Xvelocity", Mathf.Abs(horizontalInput)); // Update movement blend tree
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

    public void PickUpSword()
    {
        if (!hasSword)
        {
            hasSword = true;

            // Enable the sword in hand
            if (swordInHand != null)
            {
                swordInHand.SetActive(true);
                swordCollider = swordInHand.GetComponent<BoxCollider2D>();
            }

            // Play pickup sound
            if (audioSource != null && swordPickupSound != null)
            {
                audioSource.PlayOneShot(swordPickupSound);
            }
        }
    }

    // Ground check using collision detection
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

    // Trigger for sword pickup
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            swordnearby = true;
            swordonground = other.gameObject;
        }
    }
   
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            swordnearby = false;
        }
    }
}

