using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    #region Variables


    #endregion

    #region Referencias

    WeaponSlotManager weaponSlotManager;
    public WeaponItem righWeapon;
    public WeaponItem LeftWeapon;

    #endregion

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        weaponSlotManager.LoadWeaponOnSlot(righWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(LeftWeapon, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
