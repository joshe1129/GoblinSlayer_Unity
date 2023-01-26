using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateManager
{
    #region Variables

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;


    #endregion

    #region Referencias

    public CombatState combatState;

    #endregion
    public override StateManager Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
        HandleRotationtoTarget(enemyManager);
        //check for attack range
        //change to attack 
        if (enemyManager.isPerformingAction)
        {
            return combatState;
        }
        if (currentAttack != null)
        {
            if (distanceFromTarget < currentAttack.minimumDistanceNeededtoAttack)
            {
                return this;
            }
            else if (distanceFromTarget < currentAttack.maximumDistanceNeededtoAttack)
            {
                if (viewableAngle <= currentAttack.maximumAttackAngle && viewableAngle >= currentAttack.minumimAttackAngle)
                {
                    if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false)
                    {
                        enemyAnimationHandler.animControll.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyAnimationHandler.animControll.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);//just in case
                        enemyAnimationHandler.PlayTargetAnimation(currentAttack.actionAnimations, true);
                        enemyManager.isPerformingAction = true;
                        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                        currentAttack = null;
                        return combatState;
                    }
                }
            }
        }
        else
        {
            GetNewAttack(enemyManager);
        }   

        return combatState;

    }




    private void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        int maxScore = 0;
        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];
            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededtoAttack && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededtoAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumDistanceNeededtoAttack && viewableAngle >= enemyAttackAction.minimumDistanceNeededtoAttack )
                {
                    maxScore += enemyAttackAction.attackScore;
                }

            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];
            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededtoAttack && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededtoAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumDistanceNeededtoAttack && viewableAngle >= enemyAttackAction.minimumDistanceNeededtoAttack)
                {
                    if (currentAttack != null)
                    {
                        return;
                    }
                    
                    temporaryScore += enemyAttackAction.attackScore;
                    if (temporaryScore > randomValue)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }

            }
        }

    }


    private void HandleRotationtoTarget(EnemyManager enemyManager)
    {
        if (enemyManager.isPerformingAction)//rotate manually
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = enemyManager.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);

        }
        else//rotate with pathfinding
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;
            enemyManager.navMeshAgent.enabled = true;

            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidbody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }



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
