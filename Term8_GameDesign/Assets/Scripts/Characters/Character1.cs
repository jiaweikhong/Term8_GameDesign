using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

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

        if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.Jump)))
        {
            Debug.Log("jump pressed!");
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
}
