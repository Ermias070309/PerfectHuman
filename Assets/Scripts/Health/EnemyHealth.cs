

using UnityEngine;
public class EnemyHealth : MonoBehaviour 
{ 
    public int health = 2;
    public int damage = 1;
   public void TakeDamage(int damage) 
    { 
        health -= damage; 
        Debug.Log(gameObject.name + "  got injured, Health left: " + health); 
        
            if (health <= 0) {
             Die();
                           }
    }
    void Die()
    {
        Debug.Log(gameObject.name + "dead!"); Destroy(gameObject); 
    }
}
