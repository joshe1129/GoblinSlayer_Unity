using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region Variables
    //variables to store the input values
    public float horizontal;
    public float vertical;
    public float mouseX;
    public float mouseY;
    public float moveAmount;
    public bool b_input;
    public bool rb_Input;
    public bool rt_Input;
    public bool rollflag;
    public float rollInputTimer;
    public bool sprintFlag;
    public bool comboFlag;
    Vector2 movementInput;
    Vector2 cameraInput;
    

    #endregion

    #region Referencias
    PlayerControls inputActions; //reference to the player control class to use his methods
    PlayerAttacker playerAttacker;
    PlayerInventory playerInvertory;
    PlayerManager playerManager;

    #endregion

    private void Awake()
    {
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInvertory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
    }


    public void OnEnable()//assign the input actions created with the input system
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
        HandleRollingInput(delta);
        HandleAttackInput(delta);
    }


    private void MoveInput(float delta) //function to read and store the inputs in variables to use them
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;

    }

    private void HandleRollingInput(float delta)
    {
        b_input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;//when press the key set the bool to true
        if (b_input)
        {
            rollInputTimer += delta;
            sprintFlag = true;
        }
        else
        {
            if (rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                sprintFlag = false;
                rollflag = true;
            }

            rollInputTimer = 0;
        }
    }

    private void HandleAttackInput(float delta)
    {
        inputActions.PlayerActions.RB.performed += i => rb_Input = true;
        inputActions.PlayerActions.RT.performed += i => rt_Input = true;

        if (rb_Input)
        {
            if (playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(playerInvertory.righWeapon);
                comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                {
                    return;
                }
                if (playerManager.canDoCombo)
                {
                    return;
                }
                playerAttacker.HandleSwordAttack(playerInvertory.righWeapon);
            }
        }
        if (rt_Input)
        {
            playerAttacker.HandleShieldAttack(playerInvertory.righWeapon);
        }

    }



}
