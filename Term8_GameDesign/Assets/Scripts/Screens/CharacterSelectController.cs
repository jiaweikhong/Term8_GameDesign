using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using UnityEngine.InputSystem;

public class CharacterSelectController : MonoBehaviour
{
    public int playerNum;
    public PlayerStats playerStats;
    public CharacterSelectUI characterSelectUI;
    private ControlsManager controlsManager;
    private ScreensTransitionManager screensTransitionManager;
    private CharacterSelectManager characterManager;
    private int characterIndex = 0;
    private bool playerReady = false;

    private Vector2 navigateVector = new Vector2(0, 0);

    void Start()
    {
        // get reference and display default
        controlsManager = FindObjectOfType<ControlsManager>();
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        characterManager = FindObjectOfType<CharacterSelectManager>();
        characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
        characterSelectUI.UpdateSelected(playerReady);
    }

    // Note: The following 3 functions NavigateInput, SelectInput, and CancelInput are called from the PlayerInputScript

    public void NavigateInput(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy && context.performed && !playerReady)
        {
            // browse characters
            navigateVector = context.ReadValue<Vector2>();
            if (navigateVector.x < -0.5f)
            {
                characterIndex -= 1;
                if (characterIndex < 0) characterIndex = 3;
                characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
            }
            else if (navigateVector.x > 0.5f)
            {
                characterIndex += 1;
                if (characterIndex > 3) characterIndex = 0;
                characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
            }
        }
    }

    public void SelectInput()
    {
        if (gameObject.activeInHierarchy && !playerReady)
        {
            // if character not taken
            if (characterManager.SelectCharacter(characterIndex))
            {
                playerReady = true;
                playerStats.CharacterData = characterManager.GetCharacter(characterIndex);
                characterSelectUI.UpdateSelected(true);
                playerStats.ResetGame();
                screensTransitionManager.ReadyPlayer(true);
            }
        }
    }

    public void CancelInput()
    {
        if (gameObject.activeInHierarchy && playerReady)
        {
            // undo selection
            playerReady = false;
            characterManager.UnSelectCharacter(characterIndex);
            characterSelectUI.UpdateSelected(false);
            screensTransitionManager.ReadyPlayer(false);
        }
    }
}
