using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.EventSystems;

public class Murasaki : GenericCharacter
{
    public Animator potionAnimator;
    // If you want to override awake, please see: https://answers.unity.com/questions/388454/can-ishould-i-call-awake-in-parent-class-manually.html
    public Transform firePoint;
    public Transform swordsSpawnPoint;
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
        // potionAnimator.SetTrigger("Primary");

        // Set casterPlayerNum in primaryPotion script of prefab 
        GameObject primaryPotion = ObjectPooler.SharedInstance.GetPooledObject("MurasakiPrimary(Clone)"); 
        // Instantiate(primaryPotionPrefab, firePoint.position, firePoint.rotation);
        primaryPotion.GetComponent<PrimaryPotion>().casterPlayerNum = playerScript.playerNum;
        if (primaryPotion != null)
        {
            primaryPotion.transform.position = firePoint.position;
            primaryPotion.transform.rotation = firePoint.rotation;
            primaryPotion.SetActive(true);
            // primaryPotion.GetComponent<PrimaryPotion>().enabled = true;
        }
        
        Debug.Log(playerScript.playerNum + " Potion 1!!");
    }

    public override void UsePotion2()
    {
        animator.SetTrigger("Attack");

        GameObject secondaryPotion = ObjectPooler.SharedInstance.GetPooledObject("MurasakiSecondary(Clone)"); 
        secondaryPotion.GetComponent<SecondaryPotion>().casterPlayerNum = playerScript.playerNum;
        if (secondaryPotion != null)
        {
            secondaryPotion.transform.position = swordsSpawnPoint.position;
            secondaryPotion.transform.rotation = swordsSpawnPoint.rotation;
            secondaryPotion.SetActive(true);
        }
        Debug.Log("Potion 2!!");
    }

    public override void UsePotion3()
    {
        // remember to check if there's any more potions left.
        Debug.Log("Potion 3!");
        // SwiftnessElixir();
        // KillerBrew();
        // MuddlingMist();
        DreamDust();
    }

    public override void OnDeath()
    {
        // trigger death animation
        animator.SetTrigger("Death");
    }
}
