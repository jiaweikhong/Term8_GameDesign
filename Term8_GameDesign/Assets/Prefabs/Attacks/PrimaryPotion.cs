using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryPotion : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int casterPlayerNum;
    
    void OnEnable()
    {
        Debug.Log("I waz enabled");
        rb.velocity = transform.right * speed;
        StartCoroutine((Shoot()));
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("I shooted but it no werk");
        gameObject.SetActive(false);
    }
    
    // void OnTriggerEnter2D(Collider2D other) {
    //     Debug.Log("primary potion hit smth" + other.name  + other.tag);
        
    // }
}
