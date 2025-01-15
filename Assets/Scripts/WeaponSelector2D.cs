using UnityEngine;



public class WeaponSelector2D : MonoBehaviour
{
    [Header("Weapon References")]
    public GameObject knife;      // Reference to the knife
    public GameObject gloves;     // Reference to the gloves
    public GameObject pistol;     // Reference to the pistol

    private GameObject currentWeapon; // The currently equipped weapon

    private Rigidbody2D player; // Rigidbody2D component for the player

    [Header("Pickup Settings")]
    public float pickupRadius = 1f; // Radius for picking up weapons
    private GameObject nearbyWeapon; // Stores the nearest weapon within the pickup radius

    [Header("Sound Settings")]
    public AudioSource audioSource;       // AudioSource component for playing sounds
    public AudioClip pickupSound;         // Sound effect for picking up weapons
    public AudioClip walkingSound;        // Sound effect for walking

    private bool isWalking = false;       // To track if walking sound is playing

    void Start()
    {
        player = GetComponent<Rigidbody2D>();

        // Check if all weapons are assigned in the Inspector
        if (gloves == null || knife == null || pistol == null)
        {
            Debug.LogError("One or more weapons are not assigned in the Inspector!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
        }
    }

    void Update()
    {
        HandleWalkingSound();

        // Check if the player presses 'E' and a weapon is nearby
        if (Input.GetKeyDown(KeyCode.E) && nearbyWeapon != null)
        {
            EquipWeapon(nearbyWeapon); // Equip the nearest weapon
        }
    }

    // Detect when the player is near a weapon
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("knife") || collider.gameObject.CompareTag("pistol") || collider.gameObject.CompareTag("gloves"))
        {
            nearbyWeapon = collider.gameObject; // Store the nearby weapon
            Debug.Log("Weapon nearby: " + nearbyWeapon.name);
        }
    }

    // Detect when the player leaves the weapon's area
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == nearbyWeapon)
        {
            nearbyWeapon = null; // Clear the reference when leaving the weapon
            Debug.Log("Weapon left: " + collider.gameObject.name);
        }
    }

    void EquipWeapon(GameObject newWeapon)
    {
        // Deactivate the current weapon if there is one
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(true);
        }

        // Equip the new weapon
        currentWeapon = newWeapon;
        currentWeapon.SetActive(false);

        // Play the pickup sound
        PlayPickupSound();

        Debug.Log("Equipped weapon: " + newWeapon.name);
    }

    void PlayPickupSound()
    {
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound); // Play the pickup sound once
        }
        else
        {
            Debug.LogWarning("Pickup sound or AudioSource is not assigned!");
        }
    }

    void HandleWalkingSound()
    {
        // Check horizontal movement input
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            if (!isWalking && walkingSound != null && audioSource != null)
            {
                // Start playing walking sound
                audioSource.clip = walkingSound;
                audioSource.loop = true; // Loop walking sound
                audioSource.Play();
                isWalking = true;
            }
        }
        else
        {
            // Stop walking sound when not moving
            if (isWalking && audioSource.isPlaying)
            {
                audioSource.Stop();
                isWalking = false;
            }
        }
    }

    // Draw a circle in the scene to visualize the pickup radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}


