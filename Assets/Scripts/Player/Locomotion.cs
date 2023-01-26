using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    #region Variables
    //variables para controlar el movimiento del personaje
    [Header("Ground and Air Stats")]

    [SerializeField] float groundDetectionrayStart = 0.5f;
    [SerializeField] float minimunDistancetoFall = 1f;
    [SerializeField] float groundRayDistance = 0.2f;
    LayerMask ignoreForGroundCheck;
    public float inAirTimer;


    [Header("Movement Variables")]
    Vector3 normalVector;
    Vector3 targetPosition;
    public Vector3 moveDirection;

    [Header("Movement Stats")]
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float walkSpeed = 1;
    [SerializeField] float rotationSpeed = 8;
    [SerializeField] float springSpeed = 7;
    [SerializeField] float fallingSpeed = 9.81f;

    #endregion

    #region Referencias

    Transform cameraObject;
    InputHandler inputHandler;
    public Rigidbody myRigidbody;
    public GameObject normalCamera;
    PlayerManager playerManager;
    [HideInInspector] public Transform myTransform;
    [HideInInspector] public PlayerAnimationHandler animationHandler;
    public CapsuleCollider characterCollader;
    public CapsuleCollider characterCollisionBlockerCollader;
    #endregion
    // Start is called before the first frame update
    void Start()
    {

        playerManager = GetComponent<PlayerManager>();
        myRigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animationHandler = GetComponentInChildren<PlayerAnimationHandler>();
        animationHandler.Initialize();
        playerManager.isGrounded = true;
        ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
        Physics.IgnoreCollision(characterCollader, characterCollisionBlockerCollader, true);


    }
    #region Movement

    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = myTransform.forward;
        }

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }

    public void HandleMovement(float delta)
    {
        if (inputHandler.rollflag)
        {
            return;
        }
        if (playerManager.isInteracting)
        {
            return;
        }
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = movementSpeed;

        if (inputHandler.sprintFlag && inputHandler.moveAmount > 0.5)
        {
            speed = springSpeed;
            playerManager.isSprinting = true;
            moveDirection *= speed;
        }
        else
        {
            if (inputHandler.moveAmount < 0.5)
            {
                moveDirection *= walkSpeed;
                playerManager.isSprinting = false;
            }
            else
            {
                moveDirection *= speed;
                playerManager.isSprinting = false;
            }
        }

       Vector3 playerVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
       myRigidbody.velocity = playerVelocity;

        animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);


        if (animationHandler.canRotate)
        {
            HandleRotation(delta);
        }

    }

    public void HandleRollingandSprint(float delta)
    {
        if (animationHandler.animControll.GetBool("isInteracting"))//avoid the rolling animation if the character is doing something else
        {
            return;
        }
        if (inputHandler.rollflag)//decide the move direction of the rolling
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;

            if (inputHandler.moveAmount > 0)
            {
                animationHandler.PlayTargetAnimation("Rolling", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
            }
            else
            {
                animationHandler.PlayTargetAnimation("BackStep", true);
            }
        }
        

    }

    public void HandleFalling(float delta, Vector3 moveDirection)
    {
        playerManager.isGrounded = false;
        RaycastHit hit;
        Vector3 origin = myTransform.position;
        origin.y += groundDetectionrayStart;

        if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }

        if (playerManager.isInAir)
        {
            myRigidbody.AddForce(-Vector3.up * fallingSpeed);
            myRigidbody.AddForce(moveDirection * fallingSpeed / 8);//efect of push if you walk out a edge
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * groundRayDistance;

        targetPosition = myTransform.position;
        Debug.DrawRay(origin, -Vector3.up * minimunDistancetoFall, Color.red, 0.1f, false);
        if (Physics.Raycast(origin, -Vector3.up, out hit, minimunDistancetoFall, ignoreForGroundCheck))
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y;

            if (playerManager.isInAir)
            {
                if (inAirTimer > 0.5f)
                {
                    Debug.Log("you were in the air" + inAirTimer);
                    animationHandler.PlayTargetAnimation("Land",true);
                    inAirTimer = 0;
                }
                else
                {
                    animationHandler.PlayTargetAnimation("Empty", false);
                    inAirTimer = 0;
                }

                playerManager.isInAir = false;
            }
        }
        else
        {
            if (playerManager.isGrounded)
            {
                playerManager.isGrounded = false;
            }
            if (playerManager.isInAir == false)
            {
                if (playerManager.isInteracting == false)
                {
                    animationHandler.PlayTargetAnimation("Falling", true);
                }

                Vector3 vel = myRigidbody.velocity;
                vel.Normalize();
                myRigidbody.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;

            }
        }

        if (playerManager.isGrounded)
        {
            if (playerManager.isInteracting || inputHandler.moveAmount > 0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                myTransform.position = targetPosition;
            }
        }

    }

    #endregion


}
