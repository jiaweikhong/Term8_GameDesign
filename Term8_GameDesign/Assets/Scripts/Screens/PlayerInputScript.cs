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
            if (screensTransitionManager.GetScreenNum() == 0)
            {
                screensTransitionManager.onSelectPlay();
            }

            else if (screensTransitionManager.GetScreenNum() == 1)
            {
                characterSelectController.SelectInput();
                //controlsManager.SwitchAllControllersToCharacterMode();
                //SceneManager.LoadScene(1);
            }

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
            if (screensTransitionManager.GetScreenNum() == 1)
            {
                characterSelectController.CancelInput();
            }

            else if (screensTransitionManager.GetScreenNum() == 2)
            {
                brewingPhaseController.CancelInput(context);
            }
        }
    }

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