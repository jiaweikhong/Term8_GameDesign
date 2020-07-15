using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Character1 : GenericCharacter
{   
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

    void Awake()
    {
        base.getComponents();
        movementController = GetComponent<CharacterMovementController>();
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

        if (Input.GetKey(controlsManager.GetKey(playerScript.playerNum, ControlKeys.Jump)))
        {
            jump = true;
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
        // TODO: attack animation
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
    }

    public override void usePotion3()
    {
        // remember to check if there's any more potions left. it's stored in base.playerScript.qtyPotion3
        Debug.Log("Potion 3!");
        
        if (base.playerScript.qtyPotion3 > 0)
        {
            // do the potion
        }
    }

    public override void onDeath()
    {
        // TODO: trigger death animation
        // dont need to increment death since it is taken care of in GameManager.takeDamage
        throw new System.NotImplementedException();
    }
}
