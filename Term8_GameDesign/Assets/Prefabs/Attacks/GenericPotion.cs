using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPotion : MonoBehaviour
{
    // hide from inspector since they are not set in Inspector
    [HideInInspector]
    public int casterPlayerNum;
    
}
