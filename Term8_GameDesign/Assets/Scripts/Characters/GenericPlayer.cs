using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayer : MonoBehaviour
{
    public int playerNum;
    public int characterNum;

    void Awake()
    {
        GameManager.OnDeathEvent += GenericPlayerDeath;         // subscribe so that GenericPlayer knows when to die
    }

    private void GenericPlayerDeath(int deadPlayerNum)
    {
        if (deadPlayerNum == playerNum)
        {
            // trigger death animation in character
            GetComponentInChildren<GenericCharacter>().OnDeath();
            Debug.Log("Player " + playerNum + " has ded");
        }
    }

    public void TakeDamage(int attackingPlayerNum)
    {
        // TODO: trigger hurt animation
        GameManager.Instance.PlayerTakesDamage(attackingPlayerNum, playerNum);
    }

    public bool UseSecondaryPotionIfCanUse()
    {
        return GameManager.Instance.UseSecondaryPotionIfCanUse(playerNum);
    }

    public bool UseSpecialPotionIfCanUse()
    {
        return GameManager.Instance.UseSpecialPotionIfCanUse(playerNum);
    }

    public void IncreaseDamageDealtTo2()
    {
        GameManager.Instance.IncreaseDamageDealtTo2(playerNum);
    }

    public void DecreaseDamageDealtTo1()
    {
        GameManager.Instance.DecreaseDamageDealtTo1(playerNum);
    }

    void AttachCharacter(int charNum)
    {
        // set base characterNum
        characterNum = charNum;
        switch (characterNum)
        {
            case 1:
                // attach character 1 prefab as a child to this player (which includes the sprite, script etc);
                break;
            case 2:
                // attach character 2 prefab as a child to this player (which includes the sprite, script etc);
                break;
            case 3:
                // attach character 3 prefab as a child to this player (which includes the sprite, script etc);
                break;
            case 4:
                // attach character 4 prefab as a child to this player (which includes the sprite, script etc);
                break;
        }
    }
}
