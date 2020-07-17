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
    public Transform attackPos;                                                                
    public float attackRangeX;      //  TODO: refactor into CharacterStats ScriptableObject?
    public float attackRangeY;      //
    public LayerMask whatIsOpponents;
    private bool wasHurted;         // To prevent issue of getting damaged multiple times by same attack

    void Awake()
    {
        base.getComponents();
        movementController = GetComponent<CharacterMovementController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
        Debug.Log("player num isss " + playerScript.playerNum);
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
                useCharacterPotion();
            }
            else if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.SecondaryKey)))
            {
                usePotion2();
            }
            else if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.SpecialKey)))
            {
                usePotion3();
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
        Debug.Log("other is " + other.gameObject);
        if (other.gameObject.CompareTag("Damage") && !wasHurted)
        {
            int otherPlayerNum = other.gameObject.transform.parent.gameObject.GetComponentInParent<GenericPlayer>().playerNum;
            Debug.Log("taken damage from  " + otherPlayerNum);
            playerScript.takeDamage(otherPlayerNum);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }

    void FixedUpdate()
    {
        // Move our character
        movementController.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        movementController.BetterJump();
    }

    public override void useCharacterPotion()
    {
        // attack animation
        animator.SetTrigger("Attack");
        Vector2 attackRange = new Vector2(attackRangeX, attackRangeY);
        Collider2D[] opponentsToDamage = Physics2D.OverlapBoxAll(attackPos.position, attackRange, 0, whatIsOpponents);
        for (int i = 0; i < opponentsToDamage.Length; i++)
        {
            Debug.Log("damage taken by player");
            opponentsToDamage[i].GetComponentInParent<GenericPlayer>().takeDamage(playerScript.playerNum);
        }
    }

    public override void usePotion2()
    {
        Debug.Log("Potion 2!!");
        // do the same check as described in usePotion3()
        // animator.SetTrigger("Attack");
    }

    public override void usePotion3()
    {
        // remember to check if there's any more potions left. it's stored in base.playerScript.qtyPotion3
        Debug.Log("Potion 3!");
        
        if (base.playerScript.qtyPotion3 > 0)
        {
            // do the potion
            // animator.SetTrigger("Attack");
        }
    }

    public override void onDeath()
    {
        // trigger death animation
        animator.SetTrigger("Death");
        // dont need to increment death since it is taken care of in GameManager.takeDamage
        // throw new System.NotImplementedException();
    }
}
