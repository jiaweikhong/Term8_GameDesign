using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(menuName = "SpecialPotionData")]
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
    [SerializeField]
    private SpecialPotionType specialPotionType;

    public string SpecialName
    {
        get
        { return specialName; }
    }
    public Sprite Sprite
    {
        get
        { return sprite; }
    }
    public int Cost
    {
        get
        { return cost; }
    }
    public SpecialPotionType SpecialPotionType
    { get
        { return specialPotionType; }
    }
}