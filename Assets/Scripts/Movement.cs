using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWithSounds : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public bool isGrounded;
    bool swordNearby = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Sound Settings")]
    public AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip jumpingSound;
    public AudioClip swordSwingSound;
    GameObject swordOnGround;
    private bool isWalking = false;

    [Header("Sword Settings")]
    public GameObject swordInHand;
    public bool hasSword = false;
    public AudioClip swordPickupSound;

    private bool isSwordAttack = false;
    private BoxCollider2D swordCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }

        if (swordInHand != null)
        {
            swordInHand.SetActive(false);
            swordCollider = swordInHand.GetComponent<BoxCollider2D>();
            if (swordCollider != null)
            {
                swordCollider.enabled = false;
            }
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleSwordActions();
        UpdateAnimatorParameters();

        if (Input.GetKeyDown(KeyCode.E) && swordNearby)
        {
            PickUpSword();
            Destroy(swordOnGround);
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0;
        }

        // Play walking sounds
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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            PlayJumpingSound();
            animator.SetTrigger("Jump");
        }
    }

    private void HandleSwordActions()
    {
        if (hasSword)
        {
            animator.SetBool("HoldingSword", true);

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
            animator.SetTrigger("fightingswordplayer");

            if (audioSource != null && swordSwingSound != null)
            {
                audioSource.PlayOneShot(swordSwingSound);
            }

            if (swordCollider != null)
            {
                swordCollider.enabled = true;
            }

            Invoke(nameof(ResetSwordAttack), 0.5f);
        }
    }

    private void ResetSwordAttack()
    {
        isSwordAttack = false;

        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }

    private void UpdateAnimatorParameters()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (hasSword && Mathf.Abs(horizontalInput) > 0.1f)
        {
            animator.SetBool("RunningWithSword", true); // Trigger running with sword animation
        }
        else
        {
            animator.SetBool("RunningWithSword", false); // Reset running with sword animation
        }

        animator.SetFloat("Xvelocity", Mathf.Abs(horizontalInput)); // Update running or idle animations
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

            if (swordInHand != null)
            {
                swordInHand.SetActive(true);
                swordCollider = swordInHand.GetComponent<BoxCollider2D>();
            }

            if (audioSource != null && swordPickupSound != null)
            {
                audioSource.PlayOneShot(swordPickupSound);
            }
        }
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            swordNearby = true;
            swordOnGround = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            swordNearby = false;
        }
    }
}

