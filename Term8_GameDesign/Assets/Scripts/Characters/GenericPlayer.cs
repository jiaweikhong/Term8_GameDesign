using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayer : MonoBehaviour
{
    public int playerNum;
    public int characterNum;
    protected GenericCharacter genericCharacter;

    void Awake()
    {
        GameManager.Instance.OnDeathEvent += GenericPlayerDeath;         // subscribe so that GenericPlayer knows when to die
        GameManager.Instance.OnMuddledEvent += PlayerMuddled;
        GameManager.Instance.OnDreamingEvent += PlayerDreaming;

        genericCharacter = GetComponentInChildren<GenericCharacter>();
    }

    // Event Handlers
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
            StartCoroutine(RevertMuddleness());
        }
    }

    private void PlayerDreaming(int casterPlayerNum)
    {
        if (casterPlayerNum != playerNum)
        {
            Debug.Log("I am dreaming, my player number is " + playerNum);
            genericCharacter.SetDreaming(true);
            StartCoroutine(RevertDreaming());
        }
    }

    IEnumerator RevertMuddleness()
    {
        yield return new WaitForSeconds(1.5f);
        genericCharacter.SetMuddleness(false);
        Debug.Log("Ended Muddling Mist on player " + playerNum);
    }

    IEnumerator RevertDreaming()
    {
        yield return new WaitForSeconds(2f);
        genericCharacter.SetDreaming(false);
        Debug.Log("Ended Dream Dust on player " + playerNum);
    }

    // Pass request to GameManager
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

    public void CastMuddlingMist()
    {
        GameManager.Instance.CastMuddlingMist(playerNum);
    }

    public void CastDreamingDust()
    {
        GameManager.Instance.CastDreamDust(playerNum);
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
