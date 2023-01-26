using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="A.I./Enemy Actions/Attack Action")]
public class EnemyAttackAction : EnemyActions
{
    #region Variables

    public int attackScore = 3;
    public float recoveryTime = 2;

    public float maximumAttackAngle = 35;
    public float minumimAttackAngle = -35;

    public float maximumDistanceNeededtoAttack = 3;
    public float minimumDistanceNeededtoAttack = 0;

    #endregion

    #region Referencias




    #endregion


}
