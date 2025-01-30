using UnityEngine;
using UnityEngine.SceneManagement;
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


       
        if(currentHealth <= 0)
        {
            GetComponent<PlayerMovementWithSounds>().enabled = false;
            SceneManager.LoadScene("Scene1");
            Destroy(gameObject);
        }
     
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}

