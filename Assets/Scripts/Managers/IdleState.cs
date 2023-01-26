using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateManager
{
    #region Variables
    public LayerMask detectionLayer;
    public LayerMask wayPoints;
    public float timer;
    public float unstucktimer;
    #endregion

    #region Referencias

    public PurseTargetState purseTargetState;//or you can use an awake method and search for it

    #endregion

 

    public override StateManager Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler)
    {

        //look for a potential target
        //switch to purse
        timer += Time.deltaTime;
        unstucktimer += Time.deltaTime;

        Collider[] waypointColliders = Physics.OverlapSphere(enemyManager.transform.position, enemyManager.detectionRadius, wayPoints);           
        
        enemyManager.navMeshAgent.enabled = true;

        if (enemyManager.navMeshAgent.remainingDistance <= 0.8)
        {
            enemyAnimationHandler.animControll.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                unstucktimer = 0;
                enemyManager.navMeshAgent.destination = waypointColliders[Random.Range(0, waypointColliders.Length)].transform.position;
                timer = 0;
            }
        }
        if (enemyManager.navMeshAgent.remainingDistance >= 1)
        {
            enemyAnimationHandler.animControll.SetFloat("Vertical", 1, 01, Time.deltaTime);
            if (unstucktimer >= 10)
            {
                unstucktimer = 0;
                enemyManager.navMeshAgent.destination = waypointColliders[Random.Range(0, waypointColliders.Length)].transform.position;
            }
         
        }

        enemyManager.enemyRigidbody.velocity = enemyManager.enemyRigidbody.velocity;
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);

        enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
        enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;




        #region Handle Enemy target detection

        //enemyAnimationHandler.animControll.SetFloat("Vertical", 0, 01, Time.deltaTime);
        Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, enemyManager.detectionRadius, detectionLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                Vector3 targetDirection = characterStats.transform.position - enemyManager.transform.position;
            /*
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    enemyManager.currentTarget = characterStats;
                }
*/
               
                    enemyManager.currentTarget = characterStats;
                


            }

        }
        #endregion
        
        if (enemyManager.currentTarget != null)
        {
            return purseTargetState;
        }
        else
        {
            return this;
        }

    }







}
