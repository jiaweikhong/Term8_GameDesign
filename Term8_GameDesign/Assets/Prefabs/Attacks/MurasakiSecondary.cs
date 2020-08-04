using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurasakiSecondary : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;

    // hide from inspector since they are not set in Inspector
    [HideInInspector]
    public int casterPlayerNum;
    [HideInInspector]
    public Vector3 casterPlayerSpeed;
    
    void OnEnable()
    {
        // add caster player's x velocity to the potion's velocity
        Vector3 playerVelocityX = Vector3.right * casterPlayerSpeed.x;
        rb.velocity = transform.right * speed + playerVelocityX;
        Invoke("Destroy", 0.45f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void SetCasterPlayerNumAndSpeed(int playerNum, Vector3 playerSpeed)
    {
        casterPlayerNum = playerNum;
        casterPlayerSpeed = playerSpeed;

    }
}
