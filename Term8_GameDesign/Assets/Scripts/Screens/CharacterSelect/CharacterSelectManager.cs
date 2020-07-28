using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public CharacterData[] characterDataList;
    private bool[] characterIsTaken;

    // Start is called before the first frame update
    void Start()
    {
        characterIsTaken = new bool[4];
        foreach (CharacterData character in characterDataList)
        {
            character.Availability = false;
        }
    }

    public CharacterData GetCharacter(int i)
    {
        return characterDataList[i];
    }

    public bool SelectCharacter(int i)
    {
        if (!characterIsTaken[i]) 
        {
            characterIsTaken[i] = true;
            return true;
        }
        else
            return false;
    }

    public void UnSelectCharacter(int i)
    {
        characterIsTaken[i] = false;
    }
}
