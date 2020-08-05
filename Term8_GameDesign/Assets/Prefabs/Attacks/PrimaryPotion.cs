using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryPotion : GenericPotion
{
    public float speed = 7f;
    public Rigidbody2D rb;

    [HideInInspector]
    public Vector3 casterPlayerSpeed;
    
    void OnEnable()
    {
        // add caster player's x velocity to the potion's velocity
        Vector3 playerVelocityX = Vector3.right * casterPlayerSpeed.x;
        rb.velocity = transform.right * speed + playerVelocityX;
        Invoke("Destroy", 0.2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
