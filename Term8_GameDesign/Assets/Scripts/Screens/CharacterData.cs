using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] 
    private string characterName;
    [SerializeField] 
    private Sprite sprite;
    [SerializeField]
    private string title;
    [SerializeField]
    private string backstory;
    [SerializeField]
    private Sprite primarySprite;
    [SerializeField]
    private string primaryName;
    [SerializeField]
    private Sprite secondarySprite;
    [SerializeField]
    private string secondaryName;

    public string CharacterName
    { get
        { return characterName; }
    }
    public Sprite Sprite
    { get
        { return sprite; }
    }
    public string Title
    { get
        { return title; }
    }
    public string Backstory
    { get
        { return backstory; }
    }
    public Sprite PrimarySprite
    { get
        { return primarySprite; }
    }
    public string PrimaryName
    { get
        { return primaryName; }
    }
    public Sprite SecondarySprite
    { get
        { return secondarySprite; }
    }
    public string SecondaryName
    { get
        { return secondaryName; }
    }
}