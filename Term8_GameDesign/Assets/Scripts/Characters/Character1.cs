using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Character1 : GenericCharacter
{
    private Animator animator;

    // movement variables
    protected CharacterMovementController movementController;
    ControlsManager controlsManager;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    private bool isLeftPressed = false;     // REMOVE THIS once we get the axis controls
    private bool isRightPressed = false;
    
    // attack variables
    private float timeBtwAttack;
    [SerializeField]
    private float startTimeBtwAttack;
    private bool wasHurted;         // To prevent issue of getting damaged multiple times by same attack

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
        //
        // remember to check if there's any more potions left.
        Debug.Log("Potion 3!");
    }

    public override void OnDeath()
    {
        // trigger death animation
        animator.SetTrigger("Death");
        // dont need to increment death since it is taken care of in GameManager.takeDamage
        // throw new System.NotImplementedException();
    }
}
