using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryPotion : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb;
    public int casterPlayerNum;
    
    void OnEnable()
    {
        rb.velocity = transform.right * speed;
        Invoke("Destroy", 0.2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
