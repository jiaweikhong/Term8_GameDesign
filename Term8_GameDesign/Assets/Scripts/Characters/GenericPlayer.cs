using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Enums;

public class GenericPlayer : MonoBehaviour
{
    public int playerNum;
    public CharacterType characterType;
    protected GenericCharacter genericCharacter;
    public GameManager gameManager;
    public GameObject DrProfessorChild;
    public GameObject LumiraChild;
    public GameObject MurasakiChild;
    public GameObject TheTravellerChild;
    public PlayerInputScript playerInputScript;
    [SerializeField]
    private ScreensTransitionManager screensTransitionManager;

    // pickup variables
    protected int weetsPickUpAmt = 20;
    protected int secPotPickUpQty = 2;

    void Awake()
    {
        genericCharacter = GetComponentInChildren<GenericCharacter>();
        screensTransitionManager.OnReturnToTitle += DetachAllCharacters;
    }

    void RetrieveChildCharacter()
    {
        genericCharacter = GetComponentInChildren<GenericCharacter>();
    }

    private void OnEnable()
    {
        gameManager.OnDeathEvent += GenericPlayerDeath;         // subscribe so that GenericPlayer knows when to die
        gameManager.OnMuddledEvent += PlayerMuddled;
        gameManager.OnDreamingEvent += PlayerDreaming;
        gameManager.OnPodiumSceneEvent += PlayerOnPodium;

    }

    // Event Handlers =============================================
    private void GenericPlayerDeath(int deadPlayerNum)
    {
        if (deadPlayerNum == playerNum)
        {
            // trigger death animation in character
            genericCharacter.OnDeath();
            Debug.Log("Player " + playerNum + " has ded");
        }
    }

    private void PlayerMuddled(int casterPlayerNum)
    {
        if (casterPlayerNum != playerNum)
        {
            Debug.Log("I am muddled, my player number is " + playerNum);
            genericCharacter.SetMuddleness(true);
            genericCharacter.allStatusEffects[0].gameObject.SetActive(true);
            StartCoroutine(RevertMuddleness());
        }
    }

    private void PlayerDreaming(int casterPlayerNum)
    {
        if (casterPlayerNum != playerNum)
        {
            Debug.Log("I am dreaming, my player number is " + playerNum);
            genericCharacter.SetDreaming(true);
            genericCharacter.allStatusEffects[1].gameObject.SetActive(true);
            StartCoroutine(RevertDreaming());
        }
    }

    private void PlayerOnPodium(bool enteringPodium)
    {
        if (genericCharacter)
        {
            if (enteringPodium)
            {
                Debug.Log("I am now in podium, my player number is " + playerNum);
                genericCharacter.isPodiumScene = true;
                // unhurt player if it was hurted
                genericCharacter.InstantUnhurtPlayer();
            }
            else
            {
                Debug.Log("I am now not in podium, my player number is " + playerNum);
                genericCharacter.isPodiumScene = false;
            }
        }
    }

    // Coroutines Methods =============================================
    IEnumerator RevertMuddleness()
    {
        yield return new WaitForSeconds(5f);
        genericCharacter.SetMuddleness(false);
        genericCharacter.allStatusEffects[0].gameObject.SetActive(false);
        Debug.Log("Ended Muddling Mist on player " + playerNum);
    }

    IEnumerator RevertDreaming()
    {
        yield return new WaitForSeconds(3f);
        genericCharacter.SetDreaming(false);
        genericCharacter.allStatusEffects[1].gameObject.SetActive(false);
        Debug.Log("Ended Dream Dust on player " + playerNum);
    }

    // Pass request to GameManager =============================================
    public void TakeDamage(int attackingPlayerNum)
    {
        gameManager.PlayerTakesDamage(attackingPlayerNum, playerNum);
    }

    public bool UseSecondaryPotionIfCanUse()
    {
        return gameManager.UseSecondaryPotionIfCanUse(playerNum);
    }

    public bool UseSpecialPotionIfCanUse()
    {
        if (GetSpecialPotionType() == SpecialPotionType.SwiftnessElixir)
        {
            // if Swiftness Elixir has been used, do not allow stacked usage 
            if (genericCharacter.isFast) return false;
        }

        return gameManager.UseSpecialPotionIfCanUse(playerNum);
    }

    public SpecialPotionType GetSpecialPotionType()
    {
        return gameManager.GetSpecialPotionType(playerNum);
    }

    public void IncreaseDamageDealtTo2()
    {
        gameManager.IncreaseDamageDealtTo2(playerNum);
    }

    public void DecreaseDamageDealtTo1()
    {
        gameManager.DecreaseDamageDealtTo1(playerNum);
    }

    public void CastMuddlingMist()
    {
        gameManager.CastMuddlingMist(playerNum);
    }

    public void CastDreamingDust()
    {
        gameManager.CastDreamDust(playerNum);
    }

    public void AddWeets()
    {
        gameManager.AddWeets(playerNum, weetsPickUpAmt);
    }

    public void AddSecPotionQty()
    {
        gameManager.AddSecPotionQty(playerNum, secPotPickUpQty);
    }

    // Attach character to player
    public void AttachCharacter(CharacterType charType)
    {
        // set base characterNum
        characterType = charType;

        // set the specified character to active (character is a child to this player gameobject)
        switch (characterType)
        {
            case CharacterType.DrProfessor:
                DrProfessorChild.SetActive(true);
                RetrieveChildCharacter();
                playerInputScript.RetrieveChildCharacter();
                break;
            case CharacterType.Lumira:
                LumiraChild.SetActive(true);
                RetrieveChildCharacter();
                playerInputScript.RetrieveChildCharacter();
                break;
            case CharacterType.Murasaki:
                MurasakiChild.SetActive(true);
                RetrieveChildCharacter();
                playerInputScript.RetrieveChildCharacter();
                break;
            case CharacterType.TheTraveller:
                TheTravellerChild.SetActive(true);
                RetrieveChildCharacter();
                playerInputScript.RetrieveChildCharacter();
                break;
        }
    }

    // set all characters to inactive
    public void DetachAllCharacters()
    {
        DrProfessorChild.SetActive(false);
        LumiraChild.SetActive(false);
        TheTravellerChild.SetActive(false);
        MurasakiChild.SetActive(false);
    }
}
