using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericCharacter : MonoBehaviour
{
    private int health;
    protected GenericPlayer playerScript;
    protected int potion3_name;
    private Rigidbody2D thisBody;

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
    }

    //abstract methods must be implemented by child classes
    public abstract void useCharacterPotion();

    public abstract void usePotion2();

    public abstract void usePotion3();

    // implement methods for all the different potion 3s here
}
