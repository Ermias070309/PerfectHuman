using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
 
=======

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
>>>>>>> Stashed changes

    public float currentHealth { get; private set; }
    private Animator anim;
    private void Awake()
    {
        currentHealth = startingHealth;
<<<<<<< Updated upstream
      
=======
        anim = GetComponent<Animator>();
>>>>>>> Stashed changes
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


       
        if(currentHealth <= 0)
        {
<<<<<<< Updated upstream
            GetComponent<PlayerMovementWithSounds>().enabled = false;
            SceneManager.LoadScene("Scene1");
            Destroy(gameObject);
=======
           anim.SetTrigger("hurt");

>>>>>>> Stashed changes
        }
     
    }

<<<<<<< Updated upstream
=======
            anim.SetTrigger("die");
        }
>>>>>>> Stashed changes

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}

