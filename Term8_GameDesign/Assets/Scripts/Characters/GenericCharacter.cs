using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericCharacter : MonoBehaviour
{
    private int health;
    protected GenericPlayer playerScript;
    protected int potion3_name;
    private bool onGround;          // TODO: REDO THE ONGROUND CHECK ITS BUGGYY
    private Rigidbody2D thisBody;
    protected float jumpForce;
    protected float moveSpeed;
    protected bool toJump = false;

    /* protected GenericCharacter (float jumpForce)
     {
         this.jumpForce = jumpForce;
     }*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected void getComponents()
    {
        playerScript = GetComponentInParent<GenericPlayer>();
        thisBody = GetComponent<Rigidbody2D>();
    }

    protected void moveCharacter()
    {
        // same movement for all 4 characters
        float horizontalMovementVal = Input.GetAxis("Horizontal");
        Vector2 movementVector = new Vector2(horizontalMovementVal * moveSpeed, thisBody.velocity.y);
        thisBody.velocity = movementVector;
    }

    protected void checkJump()
    {
        if (Input.GetKeyDown("space"))    // TODO: replace with joy con controls
        {
            Debug.Log("space is pressed");
            toJump = true;
        }
    }

    protected void jumpCharacter()
    { 
        if (onGround && toJump)
        {
            Debug.Log("passed the onGround check");
            onGround = false;
            toJump = false;
            Vector2 jumpVector = new Vector2(0, 1f);
            thisBody.AddForce(jumpVector * jumpForce, ForceMode2D.Impulse);
        }
    }

    void incrementScore()
    {
        playerScript.incrementScore();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("damage"))
        {
            health--;
        }
        /*if (health == 0)
        {
            playerScript.incrementDeath();      // not implemented yet
        }*/
        if (collision.CompareTag("ground"))
        {
            onGround = true;
            Debug.Log("touch ground");
        }
    }

    //abstract methods must be implemented by child classes
    public abstract void useCharacterPotion();

    public abstract void usePotion2();

    public abstract void usePotion3();

    // implement methods for all the different potion 3s here
}
