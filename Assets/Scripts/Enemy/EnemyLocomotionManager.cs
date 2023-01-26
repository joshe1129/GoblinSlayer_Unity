using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyLocomotionManager : MonoBehaviour
{
    #region Variables



    #endregion

    #region Referencias

    EnemyManager enemyManager;
    EnemyAnimationHandler enemyAnimationHandler;
    public CapsuleCollider characterCollader;
    public CapsuleCollider characterCollisionBlockerCollader;

    #endregion

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(characterCollader, characterCollisionBlockerCollader, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /* paso a ser parte de la state machine
    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    curretTarget = characterStats;
                }
            }
        }
    }
    */


/*
    private void HandleRotationtoTarget()
    {
        if (enemyManager.isPerformingAction)//rotate manually
        {
            Vector3 direction = enemyManager.curretTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);

        }
        else//rotate with pathfinding
        {
            Debug.Log("adios mundo"+ enemyManager.curretTarget);
            Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRigidbody.velocity;
          navMeshAgent.enabled = true;

            navMeshAgent.SetDestination(enemyManager.curretTarget.transform.position);
            enemyRigidbody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }



    }
    */

}
