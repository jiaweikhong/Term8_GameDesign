using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericCharacter : MonoBehaviour
{
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

    // no use at the moment, the score is done by the gamemanager
    /*void incrementScore()
    {
        playerScript.incrementScore();
    }*/

    //abstract methods must be implemented by child classes
    public abstract void useCharacterPotion();

    public abstract void usePotion2();

    public abstract void usePotion3();

    public abstract void onDeath();

    // implement methods for all the different potion 3s here
}
