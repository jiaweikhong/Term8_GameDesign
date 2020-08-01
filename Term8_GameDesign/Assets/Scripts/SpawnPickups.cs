using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public float spawnTime = 15f;       // time between each spawns
    
    void Start()
    {
        // Invokes the spawning of pickups in spawnTime+rand seconds
        Invoke("SpawnAWeet", spawnTime + Random.Range(-2, 2));
        Invoke("SpawnAPotion", spawnTime + Random.Range(-2, 2));
    }

    void SpawnAWeet()
    {        
        // get from objectpooler
        GameObject weets = ObjectPooler.SharedInstance.GetPooledObject("WeetsPickup(Clone)");

        ResetVelocityTransform(weets);

        // set active
        weets.SetActive(true);

        // invoke this function again after spawnTime+rand seconds
        Invoke("SpawnAWeet", spawnTime + Random.Range(-2, 2));
    }

    void SpawnAPotion()
    {
        // get from objectpooler
        GameObject potion = ObjectPooler.SharedInstance.GetPooledObject("PotionPickup(Clone)");

        if (potion)
        {
            ResetVelocityTransform(potion);

            potion.SetActive(true);
        }

        // invoke this function again after spawnTime+rand seconds
        Invoke("SpawnAPotion", spawnTime + Random.Range(-2, 2));
    }

    void ResetVelocityTransform(GameObject gameObject)
    {
        // reset velocity to zero
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;

        // reset transform to random x position at the top of screen
        gameObject.transform.position = new Vector3(Random.Range(-9f, 9f), 5, 0);
    }
}
