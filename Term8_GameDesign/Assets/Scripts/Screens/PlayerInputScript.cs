using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputScript : MonoBehaviour
{
    public PlayerInput pi;
    public ControlsManager controlsManager;
    public ScreensTransitionManager screensTransitionManager;
    public CharacterSelectController characterSelectController;
    public BrewingPhaseController brewingPhaseController;
    protected GenericCharacter genericCharacter;


    private void Start()
    {
        genericCharacter = GetComponentInChildren<GenericCharacter>();
        DontDestroyOnLoad(gameObject);
    }

    // The following are for UI navigation
    public void NavigateInput(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && context.performed)
        {
            if (screensTransitionManager.GetScreenNum() == 1)
            {
                characterSelectController.NavigateInput(context);
            }

            else if (screensTransitionManager.GetScreenNum() == 2)
            {
                // brewing controls
                brewingPhaseController.NavigateInput(context);
            }
        }
    }

    public void SubmitInput(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && context.performed)
        {
            // title screen
            if (screensTransitionManager.GetScreenNum() == 0)
            {
                screensTransitionManager.onSelectPlay();
            }
            // char select screen
            else if (screensTransitionManager.GetScreenNum() == 1)
            {
                characterSelectController.SelectInput();
            }
            // brew screen
            else if (screensTransitionManager.GetScreenNum() == 2)
            {
                brewingPhaseController.SubmitInput(context);
            }
        }
    }

    public void CancelInput(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && context.performed)
        {
            // char select screen
            if (screensTransitionManager.GetScreenNum() == 1)
            {
                characterSelectController.CancelInput();
            }
            // brew screen
            else if (screensTransitionManager.GetScreenNum() == 2)
            {
                brewingPhaseController.CancelInput(context);
            }
        }
    }

    // The following are for Arena controls
    public void MoveInput(InputAction.CallbackContext context)
    {
        genericCharacter.MoveInput(context);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            genericCharacter.JumpInput(context);
        }
    }

    public void PriPotInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            genericCharacter.PriPotInput(context);
        }
    }

    public void SecPotInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            genericCharacter.SecPotInput(context);
        }
    }

    public void SpecialPotInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            genericCharacter.SpecialPotInput(context);
        }
    }

}