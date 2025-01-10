using UnityEngine;

public class WeaponSelector2D : MonoBehaviour
{
    [Header("Vapenreferenser")]
    public GameObject knife;      // Referens till kniven
    public GameObject gloves;     // Referens till "gloves" (tidigare "bareHands")
    public GameObject pistol;     // Referens till pistolen

    private GameObject currentWeapon; // Det aktuella vapnet som spelaren h�ller

    private Rigidbody2D player; // Rigidbody2D-komponenten f�r spelaren

    [Header("Pickup-inst�llningar")]
    public float pickupRadius = 1f; // Radie f�r att plocka upp vapen
    private GameObject nearbyWeapon; // F�r att lagra det n�rmaste vapnet inom pickup-radien

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        
        // Kontrollera om alla vapen �r tilldelade i Inspector
        if (gloves == null || knife == null || pistol == null)
        {
            Debug.LogError("Ett eller flera vapen �r inte tilldelade i Inspector!");
            return;
        }
    }

    void Update()
    {
        // Kontrollera om spelaren trycker p� 'E' och har ett vapen n�ra
        if (Input.GetKeyDown(KeyCode.E) && nearbyWeapon != null)
        {
            EquipWeapon(nearbyWeapon); // Utrusta det n�rmaste vapnet
        }
    }

    // Kollar om spelaren kommer n�ra ett vapen och aktiverar pickup-logik
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("knife") || collider.gameObject.CompareTag("pistol") || collider.gameObject.CompareTag("gloves"))
        {
            // Om spelaren �r n�ra vapnet, lagra det som n�rmast
            nearbyWeapon = collider.gameObject;
            Debug.Log("Vapen i n�rheten: " + nearbyWeapon.name);
        }
    }

    // N�r spelaren l�mnar omr�det f�r vapnet
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == nearbyWeapon)
        {
            nearbyWeapon = null; // Rensa referensen om spelaren l�mnar vapnet
            Debug.Log("Vapen l�mnat: " + collider.gameObject.name);
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

    // Rita en cirkel i scenen f�r att visualisera pickup-radien
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
