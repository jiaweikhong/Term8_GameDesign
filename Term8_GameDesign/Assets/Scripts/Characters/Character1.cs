using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.EventSystems;

public class Character1 : GenericCharacter
{
    // movement variables
    protected CharacterMovementController movementController;
    ControlsManager controlsManager;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    bool isLeftPressed = false;
    bool isRightPressed = false;

    void Awake()
    {
        base.getComponents();
        movementController = GetComponent<CharacterMovementController>();
    }

    void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
    }

    void Update()
    {
        isLeftPressed = Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.LeftKey));
        isRightPressed = Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.RightKey));
        horizontalMove = isLeftPressed ? -1 : 0;
        horizontalMove = isRightPressed ? 1 : horizontalMove;
        horizontalMove *= runSpeed;

        // This allows movement with joycons. Comment out to allow keyboard movement.
        //moveCharWithJoycon();
        
        // Debug Purposes only
        //checkPress();

        if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.Jump)))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // Move our character
        movementController.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        movementController.BetterJump();
    }

    public override void useCharacterPotion()
    {
        throw new System.NotImplementedException();
    }

    public override void usePotion2()
    {
        // do the same check as described in usePotion3()
        throw new System.NotImplementedException();
    }

    public override void usePotion3()
    {
        // remember to check if there's any more potions left. it's stored in base.playerScript.qtyPotion3
        // e.g.
        if (base.playerScript.qtyPotion3 > 0)
        {
            // do the potion
        }
        throw new System.NotImplementedException();
    }

    // Allows for joycon movement
    private void moveCharWithJoycon()
    {
        float horMovement = controlsManager.moveHorizontal(playerScript.playerNum);
        horizontalMove = horMovement * runSpeed;
    }

    void checkPress()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            Debug.Log("joy 1 button 0");
        }
        else if (Input.GetKey(KeyCode.Joystick2Button0))
        {
            Debug.Log("joy 2 button 0");
        }
        else if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            Debug.Log("joy 1 button 3");
        }
    }

}
