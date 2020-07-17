using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Character2 : GenericCharacter
{
    public Animator potionAnimator;     // can refactor into the GenericCharacter (when other attacks are done)

    void FixedUpdate()
    {
        // Move our character
        movementController.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        movementController.BetterJump();
    }

    public override void UseCharacterPotion()
    {
        animator.SetTrigger("Attack");
        potionAnimator.SetTrigger("Primary");
        Debug.Log(playerScript.playerNum + " Potion 1!!");
    }

    public override void UsePotion2()
    {
/*        if (playerScript.UseSecondaryPotionIfCanUse())
        {

        }*/
        animator.SetTrigger("Attack");
        potionAnimator.SetTrigger("Secondary");
        Debug.Log(playerScript.playerNum + " Potion 2!!");
    }

    public override void UsePotion3()
    {
        // remember to check if there's any more potions left
        Debug.Log(playerScript.playerNum + "Potion 3!");
        //SwiftnessElixir();
        //KillerBrew();
        playerScript.CastMuddlingMist();
    }

    public override void OnDeath()
    {
        animator.SetTrigger("Death");
        // throw new System.NotImplementedException();
    }
}
