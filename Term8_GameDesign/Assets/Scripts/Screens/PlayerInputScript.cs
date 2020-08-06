﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputScript : MonoBehaviour
{
    public ControlsManager controlsManager;
    public ScreensTransitionManager screensTransitionManager;
    public TitleController titleController;
    public InstructionsController instructionsController;
    public ControlsController controlsController;
    public CharactersController charactersController;
    public CharacterSelectController characterSelectController;
    public BrewingPhaseController brewingPhaseController;
    public AfterMatchController afterMatchController;
    public PodiumOverlayController podiumOverlayController;
    // protected GenericCharacter genericCharacter;
    public PauseMenu pauseMenu;
    [SerializeField] protected GenericCharacter genericCharacter;


    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        genericCharacter = GetComponentInChildren<GenericCharacter>();
    }

    public void RetrieveChildCharacter()
    {
        genericCharacter = GetComponentInChildren<GenericCharacter>();
    }

    // The following are for UI navigation
    public void NavigateInput(InputAction.CallbackContext context)
    {
        // Title Screen Scene
        if (SceneManager.GetActiveScene().buildIndex == 0 && context.performed)
        {
            
            if (screensTransitionManager.GetScreenNum() == -3)
            {
                charactersController.NavigateInput(context);
            }

            if (screensTransitionManager.GetScreenNum() == 0)
            {
                titleController.NavigateInput(context);
            }
            
            if (screensTransitionManager.GetScreenNum() == 1)
            {
                characterSelectController.NavigateInput(context);
            }

            else if (screensTransitionManager.GetScreenNum() == 2)
            {
                // brewing controls
                brewingPhaseController.NavigateInput(context);
            }

            else if(screensTransitionManager.GetScreenNum() == 3)
            {
                if (PauseMenu.isGamePaused)
                {
                    pauseMenu.NavigateInput(context);
                }
            }
        }
    }

    public void SubmitInput(InputAction.CallbackContext context)
    {
        // Title Screen Scene
        if (SceneManager.GetActiveScene().buildIndex == 0 && context.performed)
        {
            // controls screen
            if (screensTransitionManager.GetScreenNum() == -2)
            {
                controlsController.SubmitInput();
            }
            // instructions screen
            if (screensTransitionManager.GetScreenNum() == -1)
            {
                instructionsController.SubmitInput();
            }
            // title screen
            if (screensTransitionManager.GetScreenNum() == 0)
            {
                titleController.SubmitInput();
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
            // after match screen
            else if (screensTransitionManager.GetScreenNum() == 4)
            {
                afterMatchController.SubmitInput();
            }
            else if (screensTransitionManager.GetScreenNum() == 3)
            {
                if (PauseMenu.isGamePaused)
                {
                    pauseMenu.Select();
                }
            }
        }
    }

    public void CancelInput(InputAction.CallbackContext context)
    {
        // Title Screen Scene
        if (SceneManager.GetActiveScene().buildIndex == 0 && context.performed)
        {
            // characters screen
            if (screensTransitionManager.GetScreenNum() == -3)
            {
                charactersController.CancelInput();
            }
            // controls screen
            if (screensTransitionManager.GetScreenNum() == -2)
            {
                controlsController.CancelInput();
            }
            // instructions screen
            if (screensTransitionManager.GetScreenNum() == -1)
            {
                instructionsController.CancelInput();
            }
            // title screen
            if (screensTransitionManager.GetScreenNum() == 0)
            {
                titleController.CancelInput();
            }
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
            // arena screen
            else if (screensTransitionManager.GetScreenNum() == 3)
            {   
                // pause screen
                if (PauseMenu.isGamePaused)
                {
                    pauseMenu.CancelInput();
                }
            }
            // after match screen
            else if (screensTransitionManager.GetScreenNum() == 4)
            {
                afterMatchController.CancelInput();
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
            if (screensTransitionManager.GetScreenNum() == 4)
            {
                podiumOverlayController.UpdateReady();
            }
            else
            {
                genericCharacter.SpecialPotInput(context);
            }
        }
    }

    public void PauseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // pause button is pressed
            pauseMenu.TogglePause();
        }
    }

}