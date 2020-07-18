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
}