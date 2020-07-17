using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script takes care of interaction with PlayerStats ScriptableObject
// (i.e. health, kills, deaths, potionQty)
public class GameManager : GenericSingletonClass<GameManager>
{

    // Attach each Player's ScriptableObject
    public PlayerStats player0;
    public PlayerStats player1;
    public PlayerStats player2;
    public PlayerStats player3;
    private Dictionary<int, PlayerStats> playersHashTable;      // store reference to PlayerStats for easy retrieval

    public delegate void PlayerDeathDelegate(int deadPlayerNum);
    public static event PlayerDeathDelegate OnDeathEvent;       // to let GenericPlayer know that they dieded so need to trigger animation

    void Start()
    {
        playersHashTable = new Dictionary<int, PlayerStats> {
            { 0, player0 },
            { 1, player1 },
            { 2, player2 },
            { 3, player3 }
        }; 
    }

    void Update()
    {

    }

    // Kill/Death/Damage
    public void PlayerTakesDamage(int attackingPlayerNum, int receivingPlayerNum)
    {
        PlayerStats receivingPlayer = playersHashTable[receivingPlayerNum];
        PlayerStats attackingPlayer = playersHashTable[attackingPlayerNum];
        receivingPlayer.PlayerHealth -= attackingPlayer.DamageDealtToOthers;
        if (receivingPlayer.PlayerHealth <= 0)
        {
            IncrementDeath(receivingPlayerNum);         // increment death for receiving player
            IncrementScore(attackingPlayerNum);         // increment score for attacking player   
            receivingPlayer.ResetPlayerHealth();        // reset health 
        }
    }

    public void IncrementScore(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.PlayerKills++;
    }

    public void IncrementDeath(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.PlayerDeaths++;
        OnDeathEvent?.Invoke(playerNum);        // let the respecive player know that they ded
    }

    public void IncreaseDamageDealtTo2(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.DamageDealtToOthers = 2;
    }

    public void DecreaseDamageDealtTo1(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.DamageDealtToOthers = 1;
    }

    // Potions
    public bool UseSecondaryPotionIfCanUse(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        if (requiredPlayer.SecondaryPotionQty > 0)
        {
            // decrement potion2 qty
            requiredPlayer.SecondaryPotionQty--;
            return true;
        }
        return false;
    }

    public bool UseSpecialPotionIfCanUse(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        if (requiredPlayer.SpecialPotionQty > 0)
        {
            // decrement potion3 qty
            requiredPlayer.SpecialPotionQty--;
            return true;
        }
        return false;
    }

    // When game ends, reset player scriptable object
    void OnDestroy()
    {
        ResetPlayer(player0);
        ResetPlayer(player1);
        ResetPlayer(player2);
        ResetPlayer(player3);
    }

    void ResetPlayer(PlayerStats playerStats)
    {
        // reset weets, potionQty, kills, death, health
        playerStats.ResetGame();
    }
}
