using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliderEnemy : MonoBehaviour
{
    #region Variables

    public int currentWeaponDamage = 5;

    #endregion

    #region Referencias

    Collider damageCollider;

    #endregion

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamgaCollider()
    {
        damageCollider.enabled = true;
    }


    public void DisableDamgaCollider()
    {

        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(currentWeaponDamage);
        }   

    }

}
