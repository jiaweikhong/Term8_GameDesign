using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Character2 : GenericCharacter
{
    private Animator animator;
    public Animator primaryPotAnimator;

    // movement variables
    protected CharacterMovementController movementController;
    ControlsManager controlsManager;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    // attack variables
    private float timeBtwAttack;
    [SerializeField]
    private float startTimeBtwAttack;


    void Awake()
    {
        base.GetComponents();
        movementController = GetComponent<CharacterMovementController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
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
                Debug.Log("Pressed Primary Key");
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
        primaryPotAnimator.SetTrigger("Primary");
        Debug.Log(playerScript.playerNum + " Potion 1!!");
    }

    public override void UsePotion2()
    {
/*        if (playerScript.UsePotion2IfCanUse())
        {
            Debug.Log("I can use");
        }*/
        animator.SetTrigger("Attack");
        primaryPotAnimator.SetTrigger("Secondary");
        Debug.Log(playerScript.playerNum + " Potion 2!!");
    }

    public override void UsePotion3()
    {
        // remember to check if there's any more potions left. it's stored in base.playerScript.qtyPotion3
        Debug.Log(playerScript.playerNum + "Potion 3!");

        if (base.playerScript.qtyPotion3 > 0)
        {
            // do the potion
        }
    }

    public override void OnDeath()
    {
        animator.SetTrigger("Death");
        // throw new System.NotImplementedException();
    }
}
