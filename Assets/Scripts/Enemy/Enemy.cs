using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected Animator animControll;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected float maximumAttackRange;
    [SerializeField] protected float soundRadius;
    [SerializeField] protected float distanceVision;
    [SerializeField] protected float timer;
    [SerializeField] protected float VisionAngle;
   // [SerializeField] protected GameObject[] wayPoints;
    enum MoveModes { randomMeshLocation, randomWaypoints };
    [SerializeField] MoveModes movemode;
    protected delegate void updateDelegate();
    protected updateDelegate updateCharacter;
    [SerializeField] protected GameObject player;
    // Start is called before the first frame update
    public Transform destination;
    public Transform currentTarget;
    public Transform eyesPosition;
    public NavMeshAgent enemyNavMeshAgent;
    public MeshRenderer plane;
    [SerializeField] private bool isstoped;
    private Vector3 random_dest;
    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyNavMeshAgent.destination = randomDestination();
    }
    protected void Awake()
    {
        animControll = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //wayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
        Movement();
        updateCharacter += CheckProximity;
        updateCharacter += CheckHearing;
        updateCharacter += CheckVision;

    }
    // Update is called once per frame
    void Update()
    {
         updateCharacter?.Invoke();
    }


    void RandomMoveinMeshArea()
    {
        if (enemyNavMeshAgent.remainingDistance <= 0.1)
        {
            animControll.SetBool("isMoving", false);
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                enemyNavMeshAgent.destination = randomDestination();
                timer = 0;
            }
        }
        if (enemyNavMeshAgent.remainingDistance >= 0.8 && isstoped == false)
        {
            animControll.SetBool("isMoving", true);
        }
    }/*
    void RandomMoveWayPoints()
    {
        if (enemyNavMeshAgent.remainingDistance <= 0.1)
        {
            animControll.SetBool("ismoving", false);
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                enemyNavMeshAgent.destination = RandomWaypoint();
                timer = 0;
            }
        }
        if (enemyNavMeshAgent.remainingDistance >= 0.8 && isstoped == false)
        {
            animControll.SetBool("ismoving", true);
        }
    }
    private Vector3 RandomWaypoint()
    {
        Vector3 rand_dest;
        rand_dest = wayPoints[Random.Range(0, wayPoints.Length)].transform.position;
        return rand_dest;
    }*/
    public void Movement()
    {
        switch ((int)movemode)
        {
            case 0:
                updateCharacter += RandomMoveinMeshArea;
                break;
            case 1:
                //updateCharacter += RandomMoveWayPoints;
                break;
            default:
                break;
        }
    }
    private Vector3 randomDestination()
    {
        Vector3 rand_dest;
        rand_dest = new Vector3(Random.Range(plane.bounds.min.x, plane.bounds.max.x), 0, Random.Range(plane.bounds.min.z, plane.bounds.max.z));
        return rand_dest;
    }

    void CheckHearing()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, soundRadius, playerLayer);
        for (int i = 0; i < colliders.Length; i++)
        {           
            if (colliders[i].tag == "Player")
            {
                Debug.Log("te oigo!!!");
                currentTarget = colliders[i].transform;
                updateCharacter -= RandomMoveinMeshArea;
                enemyNavMeshAgent.destination = currentTarget.position;
            }
        }
    }

    void CheckProximity()
    {
        Vector3 targetDirection = currentTarget.position - transform.position;
        float distanceFromTarget = Vector3.Distance(currentTarget.position, transform.position);
        if (distanceFromTarget >= maximumAttackRange)
        {
            enemyNavMeshAgent.isStopped = false;
            animControll.SetBool("isMoving", true);

        }
        else if (distanceFromTarget <= maximumAttackRange)
        {
            enemyNavMeshAgent.isStopped = true;
            isstoped = true;
            animControll.SetBool("isMoving", false);
        }

    }


    void CheckVision()
    {
        Vector3 targetDirection = currentTarget.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        Ray visionray = new Ray(eyesPosition.position, targetDirection);
        if (Mathf.Abs(viewableAngle) <= VisionAngle)
        {
            if (Physics.Raycast(visionray, out RaycastHit hit, distanceVision, ~enemyLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    currentTarget = hit.transform;
                    float distanceFromTarget = Vector3.Distance(currentTarget.position, transform.position);
                    if (distanceFromTarget <= maximumAttackRange)
                    {
                        Debug.Log("attack");
                        animControll.CrossFade("GoblinAttack", 0.02f);
               
                    }
                    updateCharacter -= RandomMoveinMeshArea;
                    enemyNavMeshAgent.destination = currentTarget.position;
                }
 
            }
        }
   



    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, soundRadius);
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(animControll.GetBoneTransform(HumanBodyBones.Head).position, (player.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest).position - animControll.GetBoneTransform(HumanBodyBones.Head).position));
    }
}
