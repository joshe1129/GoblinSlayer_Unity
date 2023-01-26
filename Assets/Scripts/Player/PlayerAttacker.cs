using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    PlayerAnimationHandler animationHandler;
    InputHandler inputHandler;
    public string lastAttack;

    private void Awake()
    {
        animationHandler = GetComponentInChildren<PlayerAnimationHandler>();
        inputHandler = GetComponent<InputHandler>();
    }

    public void HandleSwordAttack(WeaponItem weapon)
    {
        animationHandler.PlayTargetAnimation(weapon.swordAttack1, true);
        lastAttack = weapon.swordAttack1;
    }

    public void HandleShieldAttack(WeaponItem shield)
    {
        animationHandler.PlayTargetAnimation(shield.shieldAttack, true);
        lastAttack = shield.shieldAttack;
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (inputHandler.comboFlag)
        {
            animationHandler.animControll.SetBool("canDoCombo", false);
            if (lastAttack == weapon.swordAttack1)
            {
                animationHandler.PlayTargetAnimation(weapon.shieldAttack, true);
            }
            if (lastAttack == weapon.shieldAttack)
            {
                animationHandler.PlayTargetAnimation(weapon.swordAttack2, true);
            }

        }
   
    }

}
