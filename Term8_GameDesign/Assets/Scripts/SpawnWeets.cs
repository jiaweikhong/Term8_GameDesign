using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeets : MonoBehaviour
{
    public float spawnTime = 15f;       // time between each spawns
    
    void Start()
    {
        // Invokes the SpawnAWeet in spawnTime+rand seconds
        Invoke("SpawnAWeet", spawnTime + Random.Range(-2, 2));
    }

    void SpawnAWeet()
    {        
        // get from objectpooler
        GameObject weets = ObjectPooler.SharedInstance.GetPooledObject("WeetsPickup(Clone)");
        
        // reset velocity, transform
        Rigidbody2D weetsRb = weets.GetComponent<Rigidbody2D>(); 
        weetsRb.velocity = Vector3.zero;
        weets.transform.position = new Vector3(Random.Range(-9f, 9f), 5, 0);

        // set active
        weets.SetActive(true);

        // invoke this function again after spawnTime+rand seconds
        Invoke("SpawnAWeet", spawnTime + Random.Range(-2, 2));
    }
}
