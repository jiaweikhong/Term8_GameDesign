using System;
using UnityEngine;

public class AfterMatchManager : MonoBehaviour
{
    public PlayerStats[] playerStatsList;
    private int[] kills = new int[4];
    private string[] ranks = {"1ST", "2ND", "3RD", "4TH"};

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
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
}
