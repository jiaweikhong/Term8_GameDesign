using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.EventSystems;

public class Character1 : GenericCharacter
{

    // If you want to override awake, please see: https://answers.unity.com/questions/388454/can-ishould-i-call-awake-in-parent-class-manually.html

    void FixedUpdate()
    {
        // Move our character
        movementController.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        movementController.BetterJump();
    }

    public override void UseCharacterPotion()
    {
        // attack animation
        animator.SetTrigger("Attack");
        // TODO: primary attack animation
    }

    public override void UsePotion2()
    {
/*        if (playerScript.UseSecondaryPotionIfCanUse())
        {

        }*/
        Debug.Log("Potion 2!!");
        // animator.SetTrigger("Attack");
    }

    public override void UsePotion3()
    {
        // remember to check if there's any more potions left.
        Debug.Log("Potion 3!");
    }

    public override void OnDeath()
    {
        // trigger death animation
        animator.SetTrigger("Death");
    }
}
