using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingPhaseManager : MonoBehaviour
{
    public SpecialPotionData[] specialPotionList;

    public int NumSpecialPotions()
    {
        return specialPotionList.Length;
    }

    public SpecialPotionData GetSpecialPotion(int index)
    {
        return specialPotionList[index];
    }
}
