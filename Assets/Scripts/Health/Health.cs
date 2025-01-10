using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
   [SerializedField] private float startingHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;

    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if (currentHealth > 0)
        {
            // Player alive :)

        }
        else
        {
            // Player dead :(
        }
      
    }
}
