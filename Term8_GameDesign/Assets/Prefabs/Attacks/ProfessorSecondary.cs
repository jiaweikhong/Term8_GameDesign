using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorSecondary : MonoBehaviour
{
    public float speed = 450f;
    public Animator potionAnimator;
    public int casterPlayerNum;
    void OnEnable()
    {
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.right.x, 1.2f) * speed);
    }
    void OnTriggerEnter2D(Collider2D other) {
        // Probably bad coding practice...
        if (other.tag != "Damage" && other.name!="Dr. Professor")
        {
            Debug.Log("Potion hit something!"+ other.name + other.tag);
            potionAnimator.SetTrigger("Explode");
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("Destroy", 2f);
        }
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
