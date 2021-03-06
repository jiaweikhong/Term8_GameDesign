﻿using System;
using UnityEngine;

public class AfterMatchManager : MonoBehaviour
{
    public PlayerStats[] playerStatsList;
    private ScreensTransitionManager screensTransitionManager;
    private int[] kills = new int[4];
    private string[] ranks = { "1ST", "2ND", "3RD", "4TH" };

    void Awake()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        screensTransitionManager.OnNewMatch += NewMatch;
    }

    public string getPlayerRank(int playerNum)
    {
        int playerKills = playerStatsList[playerNum].PlayerKills;
        int rankIndex = Array.IndexOf(kills, playerKills);
        return ranks[rankIndex];
    }

    public string getPlayerRankPodium(int playerNum)
    {
        NewMatch();
        return getPlayerRank(playerNum);
    }

    private void NewMatch()
    {
        for (int i = 0; i < playerStatsList.Length; i++)
        {
            kills[i] = playerStatsList[i].PlayerKills;
            playerStatsList[i].Weets += 300;
        }
        Array.Sort(kills);
        Array.Reverse(kills);
        OnUpdateRank?.Invoke();
    }
    
    public delegate void UpdateRank();
    public event UpdateRank OnUpdateRank;
}