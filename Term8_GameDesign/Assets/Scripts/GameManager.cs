﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{

    // Attach each Player's ScriptableObject
    public PlayerStats player1;
    public PlayerStats player2;
    public PlayerStats player3;
    public PlayerStats player4;
    private Dictionary<int, PlayerStats> playersHashTable;      // store reference to PlayerStats for easy retrieval

    public delegate void PlayerDeathDelegate(int deadPlayerNum);
    public static event PlayerDeathDelegate onDeathEvent;       // to let GenericPlayer know that they dieded so need to trigger animation

    void Start()
    {
        playersHashTable = new Dictionary<int, PlayerStats> {
            { 0, player1 },
            { 1, player2 },
            { 2, player3 },
            { 3, player4 }
        }; 
    }

    void Update()
    {

    }

    public void playerTakesDamage(int attackingPlayerNum, int receivingPlayerNum)
    {
        PlayerStats receivingPlayer = playersHashTable[receivingPlayerNum];
        receivingPlayer.PlayerHealth--;
        if (receivingPlayer.PlayerHealth == 0)
        {
            // increment death for receiving player
            incrementDeath(receivingPlayerNum);
            // increment score for attacking player
            incrementScore(attackingPlayerNum);
            // reset health 
            receivingPlayer.PlayerHealth = 3;
        }
    }

    public void incrementScore(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.PlayerKills++;
    }

    public void incrementDeath(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.PlayerDeaths++;
        onDeathEvent?.Invoke(playerNum);        // let the respecive player know that they ded
    }

    // When game ends, reset PlayerKills and PlayerDeaths
    void OnDestroy()
    {
        resetPlayer(player1);
        resetPlayer(player2);
        resetPlayer(player3);
        resetPlayer(player4);
    }

    void resetPlayer(PlayerStats playerStats)
    {
        playerStats.PlayerKills = 0;
        playerStats.PlayerDeaths = 0;
        playerStats.PlayerHealth = 3;
    }
}
