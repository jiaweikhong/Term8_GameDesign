using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

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

    void Start()
    {
        // get reference and display default
        controlsManager = FindObjectOfType<ControlsManager>();
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        characterManager = FindObjectOfType<CharacterSelectManager>();
        characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
        characterSelectUI.UpdateSelected(playerReady);
    }

    void Update()
    {
        if (!playerReady)
        {
            // browse characters
            if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.LeftKey)))
            {
                characterIndex -= 1;
                if (characterIndex<0) characterIndex = 3;
                characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
            }
            else if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.RightKey)))
            {
                characterIndex += 1;
                if (characterIndex>3) characterIndex = 0;
                characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
            }

            // select character
            if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.PrimaryKey)))
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
        else
        {
            // undo selection
            if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.SecondaryKey)))
            {
                playerReady = false;
                characterManager.UnSelectCharacter(characterIndex);
                characterSelectUI.UpdateSelected(false);
                screensTransitionManager.ReadyPlayer(false);
            }

        }
    }
}
