using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    #region Variables

    public int currentWeaponDamage = 25;

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

        if (collision.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentWeaponDamage);
            }
        }
    }

}
