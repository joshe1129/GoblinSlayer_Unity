using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    #region Variables


    #endregion

    #region Referencias

    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    DamageCollider rightHandDamageCollider;
    DamageCollider leftHandDamageCollider;

    #endregion


    private void Awake()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isleftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isrightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }


    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isleft)
    {
        if (isleft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeapondDamageCollider();
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeapondDamageCollider();
        }
    }

    public void LoadLeftWeapondDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    public void LoadRightWeapondDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    public void OpenRightDamageCollider()
    {
        rightHandDamageCollider.EnableDamgaCollider();
    }

    public void OpenLeftDamageCollider()
    {
        leftHandDamageCollider.EnableDamgaCollider();
    }

    public void CloseRightDamageCollider()
    {
        rightHandDamageCollider.DisableDamgaCollider();
    }

    public void CloseLeftDamageCollider()
    {
        leftHandDamageCollider.DisableDamgaCollider();
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
