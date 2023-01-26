using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : MonoBehaviour
{
    #region Variables

    #endregion

    #region Referencias

    WeaponHolderSlot rightWeaponSlot;
    WeaponHolderSlot leftWeaponSlot;

    public DamageColliderEnemy rightDamageCollider;
    public DamageColliderEnemy leftDamageCollider;


    #endregion


    public void OpenRightDamageCollider()
    {
        rightDamageCollider.EnableDamgaCollider();
    }

    public void OpenLeftDamageCollider()
    {
        leftDamageCollider.EnableDamgaCollider();
    }

    public void CloseRightDamageCollider()
    {
        rightDamageCollider.DisableDamgaCollider();
    }

    public void CloseLeftDamageCollider()
    {
        leftDamageCollider.DisableDamgaCollider();
    }


}
