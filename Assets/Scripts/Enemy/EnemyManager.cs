using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    #region Variables

    public bool isPerformingAction;
    public float rotationSpeed = 15;
    public float maximumAttackRange = 1.5f;
    public float maximumFollowingDistance = 20f;
    public bool isInteracting;
    public float dropRate = 0.15f;
    //public EnemyAttackAction[] enemyAttacks;
    //public EnemyAttackAction currentAttack;

    [Header("AI Settings")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;
    public float currentRecoveryTime = 0;
    #endregion

    #region Referencias

    public StateManager currentState;
    EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimationHandler enemyAnimationManager;
    EnemyStats enemyStats;
    public Rigidbody enemyRigidbody;
    public GameObject healthPotion;
    public NavMeshAgent navMeshAgent;
    public SaveData saveData;
    public GameManager gameManager;
    public CharacterStats currentTarget;
    #endregion

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationHandler>();
        enemyStats = GetComponent<EnemyStats>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody.isKinematic = false;
        navMeshAgent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRecoveryTime();

    }


    private void FixedUpdate()
    {
        HandleStateMachine();

    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            StateManager nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);
            if (nextState != null)
            {
                SwitchtoNextState(nextState);
            }
        }

        //we will use the state machine
        /* 
        if (enemyLocomotionManager.curretTarget != null)
        {
            enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.curretTarget.transform.position, transform.position);
        }
        if (enemyLocomotionManager.curretTarget == null)
        {
            enemyLocomotionManager.HandleDetection();
        }
        else if (enemyLocomotionManager.distanceFromTarget > enemyLocomotionManager.stopingDistance)
        {
            enemyLocomotionManager.HandleMovetoTarget();

        }
        else if (true)
        {
            //handle attack
            AttackTarget();
        }*/
      
    }

    private void SwitchtoNextState(StateManager state)
    {
        currentState = state;
    }

    #region Attack

    public void AttackTarget()
    {
        /*if (isPerformingAction)
        {
            return;
        }
        if (currentAttack == null)
        {
            GetNewAttack();
        }
        else
        {
            isPerformingAction = true;
            currentRecoveryTime = currentAttack.recoveryTime;
            enemyAnimationManager.PlayTargetAnimation(currentAttack.actionAnimations, true);
            currentAttack = null;
        }*/
    }

    private void GetNewAttack()
    {
        /*Vector3 targetDirection = enemyLocomotionManager.curretTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.curretTarget.transform.position, transform.position);

        int maxScore = 0;
        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];
            if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededtoAttack && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededtoAttack)
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
            if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededtoAttack && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededtoAttack)
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
        }*/

    }

    #endregion

    private void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }
        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }
    private void OnDestroy()
    {
        if (currentTarget != null)
        {
            enemyStats.expdrop = Random.Range(5, 10);
            currentTarget.GetComponent<PlayerStats>().GetExperience(enemyStats.expdrop);
            if (Random.Range(0f, 1f) <= dropRate)
            {
                Instantiate(healthPotion, enemyRigidbody.transform.position, Quaternion.identity);
            }
            saveData.numberofEnemies--;
            if (saveData.numberofEnemies <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                gameManager.ChangeScene("GameFinished");
            }
        }

    }




}
