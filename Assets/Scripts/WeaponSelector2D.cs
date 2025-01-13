using UnityEngine;

public class WeaponSelector2D : MonoBehaviour
{
    [Header("Vapenreferenser")]
    public GameObject knife;      // Referens till kniven
    public GameObject gloves;     // Referens till "gloves" (tidigare "bareHands")
    public GameObject pistol;     // Referens till pistolen

    private GameObject currentWeapon; // Det aktuella vapnet som spelaren håller

    private Rigidbody2D player; // Rigidbody2D-komponenten för spelaren

    [Header("Pickup-inställningar")]
    public float pickupRadius = 1f; // Radie för att plocka upp vapen
    private GameObject nearbyWeapon; // För att lagra det närmaste vapnet inom pickup-radien

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        
        // Kontrollera om alla vapen är tilldelade i Inspector
        if (gloves == null || knife == null || pistol == null)
        {
            Debug.LogError("Ett eller flera vapen är inte tilldelade i Inspector!");
            return;
        }
    }

    void Update()
    {
        // Kontrollera om spelaren trycker på 'E' och har ett vapen nära
        if (Input.GetKeyDown(KeyCode.E) && nearbyWeapon != null)
        {
            EquipWeapon(nearbyWeapon); // Utrusta det närmaste vapnet
        }
    }

    // Kollar om spelaren kommer nära ett vapen och aktiverar pickup-logik
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("knife") || collider.gameObject.CompareTag("pistol") || collider.gameObject.CompareTag("gloves"))
        {
            // Om spelaren är nära vapnet, lagra det som närmast
            nearbyWeapon = collider.gameObject;
            Debug.Log("Vapen i närheten: " + nearbyWeapon.name);
        }
    }

    // När spelaren lämnar området för vapnet
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == nearbyWeapon)
        {
            nearbyWeapon = null; // Rensa referensen om spelaren lämnar vapnet
            Debug.Log("Vapen lämnat: " + collider.gameObject.name);
        }
    }

    void EquipWeapon(GameObject newWeapon)
    {
        // Avaktivera det nuvarande vapnet om det finns ett
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(true);
        }

        // Utrusta det nya vapnet
        currentWeapon = newWeapon;
        currentWeapon.SetActive(false);

        Debug.Log("Utrustat vapen: " + newWeapon.name);
    }

    // Rita en cirkel i scenen för att visualisera pickup-radien
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
