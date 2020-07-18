using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;

// This Class will control the input for all Players
public class ControlsManager : MonoBehaviour
{
    public PlayerInput controlsP1;
    public PlayerInput controlsP2;
    public PlayerInput controlsP3;
    public PlayerInput controlsP4;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SwitchAllControllersToCharacterMode()
    {
        controlsP1.SwitchCurrentActionMap("CharacterActions");
        controlsP2.SwitchCurrentActionMap("CharacterActions");
        /*        controlsP3.SwitchCurrentActionMap("CharacterActions");
                controlsP4.SwitchCurrentActionMap("CharacterActions");*/
    }

    public void SwitchAllControllersToUIMode()
    {
        controlsP1.SwitchCurrentActionMap("UIActions");
        controlsP2.SwitchCurrentActionMap("UIActions");
        /*        controlsP3.SwitchCurrentActionMap("UIActions");
                controlsP4.SwitchCurrentActionMap("UIActions");*/
    }

    /*
        // for a better selection experience with joycons
        private bool naviLock1 = false;
        private bool naviLock2 = false;
        private bool naviLock3 = false;
        private bool naviLock4 = false;

        // toggle true for joycon input, false for keyboard input
        public bool joycon = false;

        [Serializable]
        private class PlayerControl
        {
            public List<keyCode> keyCodes;

            //Return the correct KeyCode is is slow when you have more than 5 Controls use Switch instead
            public KeyCode GetKeyCode(ControlKeys controlKey)
            {
                foreach (keyCode k in keyCodes)
                    if (k.controlKey == controlKey)
                        return k.key;

                return KeyCode.None;
            }
        }

        [Serializable]
        private class keyCode
        {
            public ControlKeys controlKey;
            public KeyCode key;
        }

        //The Global List for Player Controls
        [SerializeField]
        List<PlayerControl> playerControls;

        //This Class will be used by the Player to request KeyCode.
        public KeyCode GetKey(int PlayerID, ControlKeys controlKeys)
        {
            return playerControls[PlayerID].GetKeyCode(controlKeys);
        }

        public float moveHorizontal(int playerID)
        {
            float horMovement = 0;
            switch (playerID)
            {
                case 0:
                    horMovement = Input.GetAxis("HorP1");
                    break;
                case 1:
                    horMovement = Input.GetAxis("HorP2");
                    break;
                case 2:
                    horMovement = Input.GetAxis("HorP3");
                    break;
                case 3:
                    horMovement = Input.GetAxis("HorP4");
                    break;
            }
            return horMovement;
        }

        public float moveVertical(int playerID)
        {
            float verMovement = 0;
            switch (playerID)
            {
                case 0:
                    verMovement = Input.GetAxis("VerP1");
                    break;
                case 1:
                    verMovement = Input.GetAxis("VerP2");
                    break;
                case 2:
                    verMovement = Input.GetAxis("VerP3");
                    break;
                case 3:
                    verMovement = Input.GetAxis("VerP4");
                    break;
            }
            return verMovement;
        }

    *//*    void printControllers()
        {
            string[] controllers = Input.GetJoystickNames();
            for (int i = 0; i < controllers.Length; i++)
            {
                Debug.Log("PlayerID: " + i + ": " + controllers[i]);
            }
        }*/

    /*    private void Start()
        {
            printControllers();
        }*//*

        // The following are for UI navigation

        // Locks joycon from breezing through selection when user holds the joystick in one direction.
        public void LockNavigation(int playerNum)
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
        public void UnlockNavigation(int playerNum)
        {
            if (-0.5f < moveHorizontal(playerNum) && moveHorizontal(playerNum) < 0.5f && -0.5f < moveVertical(playerNum) && moveVertical(playerNum) < 0.5f)
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
        public bool CheckLock(int playerNum)
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
        public bool CheckLeftSelect(int playerNum)
        {
            if (joycon == false)
            {
                return Input.GetKeyDown(GetKey(playerNum, ControlKeys.LeftKey));
            }
            else if (joycon == true)
            {
                return moveHorizontal(playerNum) <= -0.5f && !CheckLock(playerNum);
            }
            else
            {
                return false;
            }
        }

        // returns true if user inputs "right".
        public bool CheckRightSelect(int playerNum)
        {
            if (joycon == false)
            {
                return Input.GetKeyDown(GetKey(playerNum, ControlKeys.RightKey));
            }
            else if (joycon == true)
            {
                return moveHorizontal(playerNum) >= 0.5f && !CheckLock(playerNum);
            }
            else
            {
                return false;
            }
        }

        // returns true if user inputs "up".
        public bool CheckUpSelect(int playerNum)
        {
            if (joycon == false)
            {
                return Input.GetKeyDown(GetKey(playerNum, ControlKeys.UpKey));
            }
            else if (joycon == true)
            {
                return moveVertical(playerNum) >= 0.5f && !CheckLock(playerNum);
            }
            else
            {
                return false;
            }
        }

        // returns true if user inputs "down".
        public bool CheckDownSelect(int playerNum)
        {
            if (joycon == false)
            {
                return Input.GetKeyDown(GetKey(playerNum, ControlKeys.DownKey));
            }
            else if (joycon == true)
            {
                return moveVertical(playerNum) <= -0.5f && !CheckLock(playerNum);
            }
            else
            {
                return false;
            }

        }*/
}