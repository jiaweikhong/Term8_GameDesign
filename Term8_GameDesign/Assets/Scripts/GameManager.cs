using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script takes care of interaction with PlayerStats ScriptableObject
// (i.e. health, kills, deaths, potionQty)
//public class GameManager : GenericSingletonClass<GameManager>
public class GameManager : MonoBehaviour
{

    // Attach each Player's ScriptableObject
    public PlayerStats player0;
    public PlayerStats player1;
    public PlayerStats player2;
    public PlayerStats player3;
    private Dictionary<int, PlayerStats> playersHashTable;      // store reference to PlayerStats for easy retrieval

    public delegate void PlayerDeathDelegate(int deadPlayerNum);
    public event PlayerDeathDelegate OnDeathEvent;              // to let GenericPlayer know that they dieded so need to trigger animation

    // References to all players' in-game UI
    public GameOverlayUI[] playersGameOverlayUI;

    // SpecialPotion Events
    public delegate void MuddledDelegate(int casterPlayerNum);
    public event MuddledDelegate OnMuddledEvent;
    public delegate void DreamDelegate(int casterPlayerNum);
    public event DreamDelegate OnDreamingEvent;

    // For Camera Shake effect
    public CameraShake mainCamShake;
    public bool enableCamShakeOnDeath = true;

    private ScreensTransitionManager screensTransitionManager;

    void Start()
    {
        playersHashTable = new Dictionary<int, PlayerStats> {
            { 0, player0 },
            { 1, player1 },
            { 2, player2 },
            { 3, player3 }
        };
        DontDestroyOnLoad(gameObject);
        
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        screensTransitionManager.OnNewMatch += ResetHealth;
    }

    private void ResetHealth()
    {
        for (int i =0; i<4; i++)
        {
            PlayerStats playerStats = playersHashTable[i];
            playerStats.PlayerHealth = 3;
        }
    }

    void Update()
    {

    }

    // Kill/Death/Damage ======================================
    public void PlayerTakesDamage(int attackingPlayerNum, int receivingPlayerNum)
    {
        PlayerStats receivingPlayer = playersHashTable[receivingPlayerNum];
        PlayerStats attackingPlayer = playersHashTable[attackingPlayerNum];
        Debug.Log(receivingPlayer);
        Debug.Log(attackingPlayer);
        if (receivingPlayer != attackingPlayer)
        {
            receivingPlayer.PlayerHealth -= attackingPlayer.DamageDealtToOthers;
        }
        if (receivingPlayer.PlayerHealth <= 0)
        {
            IncrementDeath(receivingPlayerNum);         // increment death for receiving player
            IncrementScore(attackingPlayerNum);         // increment score for attacking player   
            receivingPlayer.ResetPlayerHealth();        // reset health
        }
        if (receivingPlayerNum != attackingPlayerNum || receivingPlayer.PlayerHealth <= 0)
        {
            // update in-game UI
            playersGameOverlayUI[receivingPlayerNum].UpdateNumHearts(receivingPlayer.PlayerHealth);
        }
    }

    public void IncrementScore(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.PlayerKills++;
    }

    public void IncrementDeath(int playerNum)
    {
        if (mainCamShake == null)
        {
            mainCamShake = FindObjectOfType<CameraShake>();
        }
        if (enableCamShakeOnDeath)
        {
            mainCamShake.ShakeScreen();
        }
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.PlayerDeaths++;
        //Debug.Log(OnMuddledEvent.GetInvocationList().Length);
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

    // Potions ======================================
    public bool UseSecondaryPotionIfCanUse(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        if (requiredPlayer.SecondaryPotionQty > 0)
        {
            // decrement potion2 qty
            requiredPlayer.SecondaryPotionQty--;
            // update in-game UI
            playersGameOverlayUI[playerNum].UpdateSecondaryQty(requiredPlayer.SecondaryPotionQty);
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
            // update in-game UI
            playersGameOverlayUI[playerNum].UpdateSpecialQty(requiredPlayer.SpecialPotionQty);
            return true;
        }
        return false;
    }

    public SpecialPotionType GetSpecialPotionType(int playerNum)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        return requiredPlayer.SpecialPotion.SpecialPotionType;
    }

    // Trigger Special Potion Effects ======================================
    public void CastMuddlingMist(int casterPlayerNum)
    {
        OnMuddledEvent?.Invoke(casterPlayerNum);
    }

    public void CastDreamDust(int casterPlayerNum)
    {
        OnDreamingEvent?.Invoke(casterPlayerNum);
    }

    // Pickups ======================================
    public void AddWeets(int playerNum, int amt)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.Weets += amt;
    }

    public void AddSecPotionQty(int playerNum, int amt)
    {
        PlayerStats requiredPlayer = playersHashTable[playerNum];
        requiredPlayer.SecondaryPotionQty += amt;
        playersGameOverlayUI[playerNum].UpdateSecondaryQty(requiredPlayer.SecondaryPotionQty);
    }

    // When game ends, reset player scriptable object ======================================
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
