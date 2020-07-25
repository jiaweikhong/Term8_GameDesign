using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;
using UnityEditor.U2D.Path.GUIFramework;

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
        if (controlsP1.gameObject.activeSelf == true)
        { controlsP1.SwitchCurrentActionMap("CharacterActions"); }
        if (controlsP2.gameObject.activeSelf == true)
        { controlsP2.SwitchCurrentActionMap("CharacterActions"); }
        if (controlsP3.gameObject.activeSelf == true)
        { controlsP3.SwitchCurrentActionMap("CharacterActions"); }
        if (controlsP4.gameObject.activeSelf == true)
        { controlsP4.SwitchCurrentActionMap("CharacterActions"); }
    }

    public void SwitchAllControllersToUIMode()
    {
        if (controlsP1.gameObject.activeSelf == true)
        { controlsP1.SwitchCurrentActionMap("UIActions"); }
        if (controlsP2.gameObject.activeSelf == true)
        { controlsP2.SwitchCurrentActionMap("UIActions"); }
        if (controlsP3.gameObject.activeSelf == true)
        { controlsP3.SwitchCurrentActionMap("UIActions"); }
        if (controlsP4.gameObject.activeSelf == true)
        { controlsP4.SwitchCurrentActionMap("UIActions"); }
    }

    public void DisableActionMap(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                controlsP1.SwitchCurrentActionMap("DisabledActionMap");
                break;
            case 1:
                controlsP2.SwitchCurrentActionMap("DisabledActionMap");
                break;
            case 2:
                controlsP3.SwitchCurrentActionMap("DisabledActionMap");
                break;
            case 3:
                controlsP4.SwitchCurrentActionMap("DisabledActionMap");
                break;
        }
    }

    public void EnableCharacterActionMap(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                controlsP1.SwitchCurrentActionMap("CharacterActions");
                break;
            case 1:
                controlsP2.SwitchCurrentActionMap("CharacterActions");
                break;
            case 2:
                controlsP3.SwitchCurrentActionMap("CharacterActions");
                break;
            case 3:
                controlsP4.SwitchCurrentActionMap("CharacterActions");
                break;
        }
    }
}