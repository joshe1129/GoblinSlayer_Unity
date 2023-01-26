using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//handle most of the update methods and conects the scripts like the Central Control of everything
public class PlayerManager : MonoBehaviour
{
    #region Variables
    [Header("Player Flags")]
    public bool isInteracting;
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;

    #endregion

    #region Referencias
    CameraHandler cameraHandler;
    InputHandler inputHandler;
    Animator anim;
    Locomotion locomotion;
    GameManager gameManager;

    //public GameManager game_manager;


    #endregion

    private void Awake()
    {
        cameraHandler = FindObjectOfType<CameraHandler>();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponentInChildren<Animator>();
        locomotion = GetComponent<Locomotion>();
  
    }

    // Update is called once per frame
    void Update()
    {

        float delta = Time.deltaTime;
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");

        inputHandler.TickInput(delta);//pass the tick amount to the inpuHandler
        locomotion.HandleMovement(delta);
        locomotion.HandleRollingandSprint(delta);
        locomotion.HandleFalling(delta, locomotion.moveDirection);
        

    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }
    }

    private void LateUpdate()
    {
        inputHandler.rollflag = false;
        inputHandler.sprintFlag = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;

        if (isInAir)
        {
            locomotion.inAirTimer = locomotion.inAirTimer + Time.deltaTime;
        }

    }


}
