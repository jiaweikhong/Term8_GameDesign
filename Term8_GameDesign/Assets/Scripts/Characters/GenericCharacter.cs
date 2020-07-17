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

    protected void GetComponents()
    {
        playerScript = GetComponentInParent<GenericPlayer>();
        thisBody = GetComponent<Rigidbody2D>();
    }

    //abstract methods must be implemented by child classes
    public abstract void UseCharacterPotion();

    public abstract void UsePotion2();

    public abstract void UsePotion3();

    public abstract void OnDeath();

    // implement methods for all the different potion 3s here
}
