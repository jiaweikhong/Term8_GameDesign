using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.EventSystems;

public class Lumira : GenericCharacter
{
    // If you want to override awake, please see: https://answers.unity.com/questions/388454/can-ishould-i-call-awake-in-parent-class-manually.html
    public Transform firePoint;

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

        // Set casterPlayerNum, casterPlayerSpeed in primaryPotion script of prefab 
        GameObject primaryPotion = ObjectPooler.SharedInstance.GetPooledObject("LumiraPrimary(Clone)"); 
        primaryPotion.GetComponent<PrimaryPotion>().casterPlayerNum = playerScript.playerNum;
        primaryPotion.GetComponent<PrimaryPotion>().casterPlayerSpeed = rigidBody.velocity;

        // Reset transform, set pooled object to active
        if (primaryPotion != null)
        {
            primaryPotion.transform.position = firePoint.position;
            primaryPotion.transform.rotation = firePoint.rotation;
            primaryPotion.SetActive(true);
        }
        
        Debug.Log(playerScript.playerNum + " Potion 1!!");
    }

    public override void UsePotion2()
    {
        if (playerScript.UseSecondaryPotionIfCanUse()) {
            animator.SetTrigger("Secondary");
            audioSrc.PlayOneShot(potion2SFX);

            GameObject secondaryPotion = ObjectPooler.SharedInstance.GetPooledObject("LumiraSecondary(Clone)"); 
            secondaryPotion.GetComponent<SecondaryPotion>().casterPlayerNum = playerScript.playerNum;
            if (secondaryPotion != null)
            {
                secondaryPotion.transform.position = transform.position;
                secondaryPotion.transform.rotation = transform.rotation;
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
