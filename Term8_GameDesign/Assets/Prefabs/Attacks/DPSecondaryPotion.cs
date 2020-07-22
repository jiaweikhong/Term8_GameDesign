using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPSecondaryPotion : MonoBehaviour
{
    GameObject poison;
    GameObject potionBottle;
    void Start()
    {
        poison = transform.GetChild(0).gameObject;
        potionBottle= transform.GetChild(1).gameObject;
        
        potionBottle.SetActive(true);
        poison.SetActive(false);
    }
}
