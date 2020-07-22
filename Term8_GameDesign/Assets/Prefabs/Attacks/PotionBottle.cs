using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: implement object pooling for this attack (currently keep instantiating + destroying)
// TODO: figure out why the poison doesn't damage other players
public class PotionBottle : MonoBehaviour
{
    public float speed = 400f;
    GameObject poison;
    void Start()
    {
        poison = transform.parent.GetChild(0).gameObject;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.right.x, 1.2f) * speed);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {
        // Probably bad coding practice...
        if (other.tag != "Damage" && other.name!="Dr. Professor")
        {
            Debug.Log("Potion hit something!"+ other.name + other.tag);
            Explode();
    }
    }

    void Explode() 
    {
        poison.transform.position = transform.position + new Vector3(0, 0.2f, 0);
        poison.transform.rotation = transform.rotation;
        poison.SetActive(true);
        StartCoroutine(PoisonEffect());
    }

    IEnumerator PoisonEffect()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        poison.SetActive(false);
        Destroy(transform.parent.gameObject);
    }
}
