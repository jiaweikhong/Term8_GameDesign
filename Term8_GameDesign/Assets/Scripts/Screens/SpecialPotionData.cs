using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="SpecialPotionData")]
public class SpecialPotionData : ScriptableObject
{
    [SerializeField] 
    private string specialName;
    [SerializeField] 
    private string description;
    [SerializeField] 
    private Sprite sprite;
    [SerializeField]
    private int cost;

    public string SpecialName
    { get
        { return specialName; }
    }
    public Sprite Sprite
    { get
        { return sprite; }
    }
    public int Cost
    { get
        { return cost; }
    }
}