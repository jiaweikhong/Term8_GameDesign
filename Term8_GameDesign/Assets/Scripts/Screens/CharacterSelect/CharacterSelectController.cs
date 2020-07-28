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
    private ScreensTransitionManager screensTransitionManager;
    private CharacterSelectManager characterSelectManager;
    private int characterIndex = 0;
    private bool playerReady = false;

    private Vector2 navigateVector = new Vector2(0, 0);
    public GenericPlayer playerScript;
    private AudioSource audioSrc;
    public AudioClip navigateSFX;
    public AudioClip selectSFX;
    public AudioClip errorSFX;
    public AudioClip cancelSFX;

    void Start()
    {
        // get reference and display default
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        characterSelectManager = FindObjectOfType<CharacterSelectManager>();
        characterSelectUI.UpdateCharacterDisplayed(characterSelectManager.GetCharacter(characterIndex));
        characterSelectUI.UpdateSelected(playerReady);
        audioSrc = GetComponent<AudioSource>();
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
                audioSrc.PlayOneShot(navigateSFX);
                characterIndex -= 1;
                if (characterIndex < 0) characterIndex = 3;
                characterSelectUI.UpdateCharacterDisplayed(characterSelectManager.GetCharacter(characterIndex));
            }
            else if (navigateVector.x > 0.5f)
            {
                audioSrc.PlayOneShot(navigateSFX);
                characterIndex += 1;
                if (characterIndex > 3) characterIndex = 0;
                characterSelectUI.UpdateCharacterDisplayed(characterSelectManager.GetCharacter(characterIndex));
            }
        }
    }

    public void SelectInput()
    {
        if (gameObject.activeInHierarchy && !playerReady)
        {
            // if character not taken
            if (characterSelectManager.SelectCharacter(characterIndex))
            {
                audioSrc.PlayOneShot(selectSFX);
                playerReady = true;
                playerStats.CharacterData = characterSelectManager.GetCharacter(characterIndex);
                characterSelectUI.UpdateSelected(true);
                playerStats.ResetGame();
                // attach char here
                playerScript.AttachCharacter((CharacterType)characterIndex);
                screensTransitionManager.ReadyPlayer(true);
            }
            else
            {
                audioSrc.PlayOneShot(errorSFX);
            }
        }
    }

    public void CancelInput()
    {
        if (gameObject.activeInHierarchy && playerReady)
        {
            // undo selection
            audioSrc.PlayOneShot(cancelSFX);
            playerReady = false;
            characterSelectManager.UnSelectCharacter(characterIndex);
            characterSelectUI.UpdateSelected(false);
            playerScript.DetachAllCharacters();
            screensTransitionManager.ReadyPlayer(false);
        }
        else if (gameObject.activeInHierarchy && !playerReady)
        {
            audioSrc.PlayOneShot(errorSFX);
        }
    }
}
