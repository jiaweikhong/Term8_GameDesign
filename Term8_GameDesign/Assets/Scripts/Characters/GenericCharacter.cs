﻿using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericCharacter : MonoBehaviour
{
    [SerializeField]
    protected ControlsManager controlsManager;
    protected CharacterMovementController movementController;
    protected GenericPlayer playerScript;
    private Rigidbody2D thisBody;
    protected Animator animator;

    // movement variables
    public float runSpeed = 40f;
    protected float horizontalMove = 0f;
    protected bool jump = false;
    protected bool isLeftPressed = false;     // REMOVE THIS once we get the axis controls
    protected bool isRightPressed = false;

    // attack variables
    protected float timeBtwAttack;          // To prevent spamming skill
    [SerializeField]
    protected float startTimeBtwAttack = 0.3f;
    protected bool wasHurted;         // To prevent issue of getting damaged multiple times by same collider


    public virtual void Awake()
    {
        playerScript = GetComponentInParent<GenericPlayer>();
        movementController = GetComponent<CharacterMovementController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // movement
        isLeftPressed = Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.LeftKey));
        isRightPressed = Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.RightKey));
        horizontalMove = isLeftPressed ? -1 : 0;
        horizontalMove = isRightPressed ? 1 : horizontalMove;
        horizontalMove *= runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));  // set run animation

        if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.Jump)))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        // attack
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.PrimaryKey)))
            {
                UseCharacterPotion();
            }
            else if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.SecondaryKey)))
            {
                UsePotion2();
            }
            else if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.SpecialKey)))
            {
                UsePotion3();
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage") && !wasHurted)
        {
            int otherPlayerNum = other.gameObject.transform.parent.gameObject.GetComponentInParent<GenericPlayer>().playerNum;
            Debug.Log("Taken damage from Player " + otherPlayerNum);
            playerScript.TakeDamage(otherPlayerNum);
            wasHurted = true;
            StartCoroutine(UnhurtPlayer());
        }
    }

    IEnumerator UnhurtPlayer()
    {
        // during these 0.3s won't get hurt again
        yield return new WaitForSeconds(0.3f);
        wasHurted = false;
    }

    // abstract methods must be implemented by child classes
    public abstract void UseCharacterPotion();

    public abstract void UsePotion2();

    public abstract void UsePotion3();

    public abstract void OnDeath();

    // implement methods for all the different potion 3s here
}
