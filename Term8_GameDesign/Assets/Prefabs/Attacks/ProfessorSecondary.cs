using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorSecondary : MonoBehaviour
{
    public float speed = 500f;
    public Animator potionAnimator;
    public int casterPlayerNum;
    public Vector3 casterPlayerSpeed;
    void OnEnable()
    {
        Vector2 playerVelocityX = Vector2.right * casterPlayerSpeed.x;
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.right.x, 1.25f) * speed + playerVelocityX);
    }
    void OnTriggerEnter2D(Collider2D other) {
        // Probably bad coding practice...
        if (other.tag != "Damage" && other.name!="Dr. Professor" && other.tag!="pickup")
        {
            Debug.Log("Potion hit something!"+ other.name + other.tag);
            potionAnimator.SetTrigger("Explode");
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            transform.position = transform.position + new Vector3(0, 0.2f, 0);
            Invoke("Destroy", 3f);
        }
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
