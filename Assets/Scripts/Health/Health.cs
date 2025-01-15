using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
 

    private void Awake()
    {
        currentHealth = startingHealth;
      
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        //varf�r funkar bara n�r den �r p� else? och n�r health �r mindre �n 0
        if(currentHealth <= 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
        }
     
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}

