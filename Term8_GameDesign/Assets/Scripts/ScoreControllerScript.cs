using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControllerScript : GenericSingletonClass<ScoreControllerScript>
{
    //public int p1_score { get; private set; }
    //public int p2_score { get; private set; }
    //public int p3_score { get; private set; }
    //public int p4_score { get; private set; }

    // Attach each Player's ScriptableObject
    public PlayerStats player1;
    public PlayerStats player2;
    public PlayerStats player3;
    public PlayerStats player4;


    // Start is called before the first frame update
    void Start()
    {
        //p1_score = 0;
        //p2_score = 0;
        //p3_score = 0;
        //p4_score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

/*    public void Awake()
    {
        base.Awake();
    }*/

    public void incrementScore(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                //p1_score++;
                player1.PlayerKills++;
                break;
            case 2:
                //p2_score++;
                player2.PlayerKills++;
                break;
            case 3:
                //p3_score++;
                player3.PlayerKills++;
                break;
            case 4:
                //p4_score++;
                player4.PlayerKills++;
                break;
        }
    }

    public void incrementDeath(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                player1.PlayerDeaths++;
                break;
            case 2:
                player2.PlayerDeaths++;
                break;
            case 3:
                player3.PlayerDeaths++;
                break;
            case 4:
                player4.PlayerDeaths++;
                break;
        }
    }

    // When game ends, reset PlayerKills and Playerdeaths
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
