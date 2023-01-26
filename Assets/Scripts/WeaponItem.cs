using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    #region Variables
    public bool isUnarmed;

    [Header("Attack Animations")]
    public string swordAttack1;
    public string swordAttack2;
    public string swordAttack3;
    public string shieldAttack;
    public string macheteAttack;


    #endregion

    #region Referencias

    public GameObject modelPrefab;

    #endregion

}
