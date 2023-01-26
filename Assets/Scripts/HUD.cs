using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region VariableS

    public bool esc_input;
    #endregion

    #region Referencias

    PlayerControls inputActions;
    public Slider health_slide;
    public Slider berserker_Slide;
    public GameObject pause_Menu;
    public GameObject hud_Canvas;
    public GameManager gameManager;

    #endregion

    public void OnEnable()//assign the input actions created with the input system
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();

        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }



    private void Update()
    {
        HandlePauseMenu();
    }

    public void SetMaxHealth(float health)
    {
        health_slide.maxValue = health;
        health_slide.value = health;
    }
    public void SetLifeValue(float health)
    {
        health_slide.value = health;
    }
    public void SetMaxBerserker(float mana)
    {
        berserker_Slide.maxValue = mana;
        berserker_Slide.value = mana;
    }
    public void SetBerserkerValue(float mana)
    {
        berserker_Slide.value = mana;
    }

    private void HandlePauseMenu()
    {
        esc_input = inputActions.PlayerActions.Escape.phase == UnityEngine.InputSystem.InputActionPhase.Started; ;
        if (esc_input)
        {

            Cursor.lockState = CursorLockMode.None;
            hud_Canvas.SetActive(false);
            pause_Menu.SetActive(true);
            gameManager.PauseGame();
        }

    }

}
