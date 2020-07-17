using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

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

/*    // for a better selection experience with joycons
    private bool naviLock1 = false;
    private bool naviLock2 = false;
    private bool naviLock3 = false;
    private bool naviLock4 = false;

    // for checking which device is used
    private string keyboard = "keyboard";
    private string joycon = "joycon";*/

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
            //if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.LeftKey)))
            if (controlsManager.CheckLeftSelect(playerNum))
            {
                controlsManager.LockNavigation(playerNum);
                characterIndex -= 1;
                if (characterIndex < 0) characterIndex = 3;
                characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
            }
            //else if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.RightKey)))
            else if (controlsManager.CheckRightSelect(playerNum))
            {
                controlsManager.LockNavigation(playerNum);
                characterIndex += 1;
                if (characterIndex > 3) characterIndex = 0;
                characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
            }

            // select character. for joycon support, remap the player controls in inspector. Primary key is "Joystick X Button 1" where X = joycon's number (starting from 1)
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

        // allows joycon to navigate when user returns joystick to neutral
        for (int i = 0; i < 4; i++)
        {
            controlsManager.UnlockNavigation(i);
        }
    }
/*
    // Locks joycon from breezing through selection when user holds the joystick in one direction.
    void LockNavigation(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                naviLock1 = true;
                break;
            case 1:
                naviLock2 = true;
                break;
            case 2:
                naviLock3 = true;
                break;
            case 3:
                naviLock4 = true;
                break;
        }
    }

    // Unlocks joycon when user returns joystick to neutral
    void UnlockNavigation(int playerNum)
    {
        if (-0.5f < controlsManager.moveHorizontal(playerNum) && controlsManager.moveHorizontal(playerNum) < 0.5f)
        {
            switch (playerNum)
            {
                case 0:
                    naviLock1 = false;
                    break;
                case 1:
                    naviLock2 = false;
                    break;
                case 2:
                    naviLock3 = false;
                    break;
                case 3:
                    naviLock4 = false;
                    break;
            }
        }

    }

    // returns true if user's joystick is not locked
    bool CheckLock(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                return naviLock1;
            case 1:
                return naviLock2;
            case 2:
                return naviLock3;
            case 3:
                return naviLock4;
        }
        return false;
    }

    // returns true if user inputs "left".
    // param: device is either keyboard or joystick. The variables are defined at the start of this script.
    bool CheckLeftSelect(string device)
    {
        if (device == keyboard)
        {
            return Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.LeftKey));
        }
        else if (device == joycon)
        {
            return controlsManager.moveHorizontal(playerNum) <= -0.5f && !CheckLock(playerNum);
        }
        else
        {
            return false;
        }
    }

    // returns true if user inputs "right".
    // param: device is either keyboard or joystick. The variables are defined at the start of this script.
    bool CheckRightSelect(string device)
    {
        if (device == keyboard)
        {
            return Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.RightKey));
        }
        else if (device == joycon)
        {
            return controlsManager.moveHorizontal(playerNum) >= 0.5f && !CheckLock(playerNum);
        }
        else
        {
            return false;
        }
    }*/
}
