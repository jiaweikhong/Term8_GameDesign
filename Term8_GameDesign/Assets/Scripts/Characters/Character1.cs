using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : GenericCharacter
{
    public float publicJumpForce = 7;  //shown in inspector
    public float publicMoveSpeed = 7;  // shown in inspector
    //public bool onGround;

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

    // Start is called before the first frame update
    void Start()
    {
        base.getComponents();
        base.jumpForce = publicJumpForce;
        base.moveSpeed = publicMoveSpeed;
    }

    // checking of key presses go here
    void Update()
    {
        base.checkJump();
    }

    // physics related stuff go here
    private void FixedUpdate()
    {
        base.jumpCharacter();
        base.moveCharacter();
    }
}
