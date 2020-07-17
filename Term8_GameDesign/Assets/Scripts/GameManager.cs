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
        receivingPlayer.PlayerHealth--;
        if (receivingPlayer.PlayerHealth == 0)
        {
            // increment death for receiving player
            IncrementDeath(receivingPlayerNum);
            // increment score for attacking player
            IncrementScore(attackingPlayerNum);
            // reset health 
            receivingPlayer.PlayerHealth = 3;
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

    // Potions
    public bool CanUsePotion2(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        return requiredPlayer.SecondaryPotionQty > 0;
    }

    public void UsePotion2(int playerNum)
    {
        // Before calling this function, make sure to call CanUsePotion2(playerNum) !
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.SecondaryPotionQty--;
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
