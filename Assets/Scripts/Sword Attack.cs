using UnityEngine;

public class PlayerAttack : MonoBehaviour
{ 
    public int damageAmount = 1;
    public KeyCode attackKey = KeyCode.E;
    public LayerMask enemyLayer;

    void Update() 
{
    if (Input.GetKeyDown(attackKey)) 
    {
        print("Inne i get key E");
        Attack();
    }
 } 
   
    
  void Attack()
   {
     print("Inne i attack, innan raycast");
     RaycastHit hit;
     if (Physics.Raycast(transform.position, transform.forward, out hit, enemyLayer)) 
     {
            
        print("Inne i hit");
        
        EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            print("Enemy: " +enemy);
        if (enemy != null) 
        {
            print("Skada enemy");
            enemy.TakeDamage(damageAmount); 
        }
     } 
  } 
}