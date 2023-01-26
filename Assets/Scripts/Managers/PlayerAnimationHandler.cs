using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : AnimationManager
{
    #region Variables
    int vertical;
    int horizontal;
    public bool canRotate;

    #endregion

    #region Referencias
    PlayerManager playerManager;
    InputHandler inputHandler;
    Locomotion playerlocomotin;
    #endregion


    public void Initialize()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        animControll = GetComponent<Animator>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerlocomotin = GetComponentInParent<Locomotion>();
        vertical = Animator.StringToHash("Vertical");//give an id integer to a string
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {
        //parameters to play the right animations
        #region Vertical 

        float v = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1;
        }
        else if (verticalMovement < 0 && verticalMovement >-0.55f)
        {
            v = -0.55f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }

        #endregion



        #region Horizontal


        float h = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            h = -0.55f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1;
        }
        else
        {
           h = 0;
        }
        #endregion

        if (isSprinting)
        {
            v = 2;
            h = horizontalMovement;
        }

        animControll.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        animControll.SetFloat(horizontal, h, 0.1f, Time.deltaTime);

    }




    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }


    public void EnableCombo()
    {
        animControll.SetBool("canDoCombo", true);
    }
    public void DisableCombo()
    {
        animControll.SetBool("canDoCombo", false);

    }


    private void OnAnimatorMove()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (playerManager.isInteracting == false)
        {
            return;
        }
        float delta = Time.deltaTime;

        playerlocomotin.myRigidbody.drag = 0;
        Vector3 deltaPosition = animControll.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        playerlocomotin.myRigidbody.velocity = velocity;
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
