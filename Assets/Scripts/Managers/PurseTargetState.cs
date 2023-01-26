using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurseTargetState : StateManager
{
    #region Variables

    #endregion

    #region Referencias

    public CombatState combatState;
    public IdleState idleState;

    #endregion

    public override StateManager Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler)
    {
        //chase the target
        //change to combat if is in range
        if (enemyManager.isPerformingAction)
        {
            enemyAnimationHandler.animControll.SetFloat("Vertical", 0, 01, Time.deltaTime);
            return this;
        }


        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

        if (distanceFromTarget > enemyManager.maximumAttackRange)
        {
            enemyAnimationHandler.animControll.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            if (distanceFromTarget > enemyManager.maximumFollowingDistance)
            {
                enemyManager.navMeshAgent.enabled = false;
                enemyManager.currentTarget = null;
                return idleState;
            }
        }/*
        else if (distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            enemyAnimationHandler.animControll.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }*/

        HandleRotationtoTarget(enemyManager);

        enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
        enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;

        if (distanceFromTarget <= enemyManager.maximumAttackRange)
        {

            return combatState;
        }
        else
        {
            return this;
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
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);

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



}
