using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
<<<<<<< Updated upstream
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

 private void Start()
    {
   totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
=======
    [SerializeField] private Health totalhealthbar;
    [SerializeField] private Health currentealthBar;

    private void Start()
    {
        
>>>>>>> Stashed changes
    }

    private void Update()
    {
<<<<<<< Updated upstream
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
=======
        //currentealthBar.fillAmount = playerHealth.currentHealth / 10;
    }



>>>>>>> Stashed changes
}
