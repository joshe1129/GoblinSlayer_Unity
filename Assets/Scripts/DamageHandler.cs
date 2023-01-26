using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    DamageCollider rightHandDamageCollider;
    DamageCollider leftHandDamageCollider;


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
}
