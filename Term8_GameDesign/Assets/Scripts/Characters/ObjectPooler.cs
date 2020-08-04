using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class ObjectPoolItem {
  public GameObject objectToPool;
  public int amountToPool;
  public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

	void Awake()
    {
		SharedInstance = this;
	}

	// Use this for initialization
    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name == name)
            {
                return pooledObjects[i];
            }
        }

        // if don't have an inactive object ready to be pooled
        foreach (ObjectPoolItem item in itemsToPool)
        {
            string formattedName = name.Replace("(Clone)", "");
            if (item.objectToPool.name == formattedName)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    public void SetAllPickupsToInactive()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if active & is the same tag
            if (pooledObjects[i].activeInHierarchy)
            {
                if (pooledObjects[i].CompareTag("pickup"))
                {
                    pooledObjects[i].SetActive(false);
                }
                else if (pooledObjects[i].CompareTag("potion"))
                {
                    pooledObjects[i].SetActive(false);
                }
            }
        }
    }
}
