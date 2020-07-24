using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class DrProfessor : GenericCharacter
{
    public Animator potionAnimator;     // can refactor into the GenericCharacter (when other attacks are done)
    public Transform firePoint;
    // public GameObject secondaryPotion;

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
        // Set casterPlayerNum in primaryPotion script of prefab 
        GameObject primaryPotion = ObjectPooler.SharedInstance.GetPooledObject("DrProfessorPrimary(Clone)");
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
        audioSrc.PlayOneShot(potion2SFX);

        GameObject secondaryPotion = ObjectPooler.SharedInstance.GetPooledObject("DrProfessorSecondary(Clone)");
        secondaryPotion.GetComponent<ProfessorSecondary>().casterPlayerNum = playerScript.playerNum;
        if (secondaryPotion != null)
        {
            secondaryPotion.transform.position = firePoint.position;
            secondaryPotion.transform.rotation = firePoint.rotation;
            secondaryPotion.SetActive(true);
        }
        Debug.Log("Potion 2!!");


    }

    public override void OnDeath()
    {
        animator.SetBool("IsJumping", false);
        animator.SetTrigger("Death");
        float deathAnimLength = animator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(SetSpawnPosition(deathAnimLength));
        // throw new System.NotImplementedException();
    }

}
