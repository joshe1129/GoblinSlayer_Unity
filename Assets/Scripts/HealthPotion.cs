using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    #region Variables

    public int health = 10;


    #endregion

    #region Referencias
    public GameObject myGameObject;

    #endregion




    private void OnTriggerEnter(Collider collision)
    {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();    
        if (playerStats != null)
        {
            if (playerStats.currentHealth < playerStats.maxHealth)
            {
                playerStats.currentHealth += health;
                playerStats.healthBar.SetLifeValue(playerStats.currentHealth);
            }
            Object.Destroy(myGameObject, 2);
        }
    }
}
