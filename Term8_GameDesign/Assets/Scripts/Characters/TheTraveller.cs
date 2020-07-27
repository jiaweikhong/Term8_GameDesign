﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.EventSystems;

public class TheTraveller : GenericCharacter
{
    public Animator potionAnimator;
    // If you want to override awake, please see: https://answers.unity.com/questions/388454/can-ishould-i-call-awake-in-parent-class-manually.html
    public Transform firePoint;
    public Transform pillarFirePoint;

    public AudioClip potion1SFX;
    public AudioClip potion2SFX;

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
        audioSrc.PlayOneShot(potion1SFX);
        // TODO: primary attack animation
        // potionAnimator.SetTrigger("Primary");

        // Set casterPlayerNum in primaryPotion script of prefab 
        GameObject primaryPotion = ObjectPooler.SharedInstance.GetPooledObject("TheTravellerPrimary(Clone)"); 
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
        if (playerScript.UseSecondaryPotionIfCanUse()) {
            animator.SetTrigger("Secondary");
            audioSrc.PlayOneShot(potion2SFX);

            GameObject secondaryPotion = ObjectPooler.SharedInstance.GetPooledObject("TheTravellerSecondary(Clone)"); 
            secondaryPotion.GetComponent<SecondaryPotion>().casterPlayerNum = playerScript.playerNum;
            if (secondaryPotion != null)
            {
                secondaryPotion.transform.position = pillarFirePoint.position;
                secondaryPotion.transform.rotation = pillarFirePoint.rotation;
                secondaryPotion.SetActive(true);
            }
            Debug.Log("Potion 2!!");
        }
        
    }

    public override void OnDeath()
    {
        // trigger death animation
        animator.SetBool("IsJumping", false);
        animator.SetTrigger("Death");
        float deathAnimLength = animator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(SetSpawnPosition(deathAnimLength));
    }
}
