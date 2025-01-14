using UnityEngine;

public class Health : MonoBehaviour
{
    //Komentarerna är kod tll animationerna om vi vill andvända dem sen.

    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
   // private Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;
       // anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if (currentHealth > 0)
        {
            //anim.SetTrigger("hurt");
            //Player gets hurt
        }
        else
        {
           // anim.SetTrigger("die");
           //player dies
        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //TakeDamage(1);

        }
    }
}

