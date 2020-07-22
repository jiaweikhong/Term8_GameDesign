using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryPotion : MonoBehaviour
{
    public int casterPlayerNum;
    void OnEnable()
    {
        Invoke("Destroy", 2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
