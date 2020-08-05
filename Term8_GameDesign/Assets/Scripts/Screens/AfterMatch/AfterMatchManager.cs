using System;
using UnityEngine;

public class AfterMatchManager : MonoBehaviour
{
    public PlayerStats[] playerStatsList;
    private ScreensTransitionManager screensTransitionManager;
    private int[] kills = new int[4];
    private string[] ranks = { "1ST", "2ND", "3RD", "4TH" };

    // Start is called before the first frame update
    void Start()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        screensTransitionManager.OnNewMatch += NewMatch;
    }

    void OnEnable()
    {
        Debug.Log("onenable aftermatchmanager called");
        for (int i = 0; i < playerStatsList.Length; i++)
        {
            kills[i] = playerStatsList[i].PlayerKills;
        }
        Array.Sort(kills);
        Array.Reverse(kills);
    }

    public string getPlayerRank(int playerNum)
    {
        int playerKills = playerStatsList[playerNum].PlayerKills;
        int rankIndex = Array.IndexOf(kills, playerKills);
        return ranks[rankIndex];
    }

    public string getPlayerRankPodium(int playerNum)
    {
        OnEnable();
        return getPlayerRank(playerNum);
    }

    private void NewMatch()
    {
        for (int i = 0; i < playerStatsList.Length; i++)
        {
            kills[i] = playerStatsList[i].PlayerKills;
        }
        Array.Sort(kills);
        Array.Reverse(kills);
        OnUpdateRank?.Invoke();
    }
    
    public delegate void UpdateRank();
    public event UpdateRank OnUpdateRank;
}
