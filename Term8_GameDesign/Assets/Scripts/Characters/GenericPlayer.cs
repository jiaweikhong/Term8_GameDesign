using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayer : MonoBehaviour
{
    public int playerNum;
    public int characterNum;
    public int qtyPotion2;  // these store the qty of potions that the player has but does not know what potions they are
    public int qtyPotion3;  // we need to think about the menu interaction of setting the qty of potions.

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.onDeathEvent += genericPlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void genericPlayerDeath(int deadPlayerNum)
    {
        if (deadPlayerNum == playerNum)
        {
            // TODO: trigger death animation
            // perhaps getComponent GenericCharacter -> call onDeath and implement the death animation there
            Debug.Log("Player " + playerNum + " has ded");
        }
    }

    public void takeDamage(int attackingPlayerNum)
    {
        // TODO: trigger hurt animation
        GameManager.Instance.playerTakesDamage(attackingPlayerNum, playerNum);
    }

    void attachCharacter(int charNum)
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

    // no use at the moment since GameManager will increment score / death based on playerTakesDamage()
    /*    public void incrementScore()
        {
            GameManager.Instance.incrementScore(playerNum);
        }

        public void incrementDeath()
        {
            GameManager.Instance.incrementDeath(playerNum);
        }*/

}
