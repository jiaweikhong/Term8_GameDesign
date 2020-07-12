using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class CharacterSelectController : MonoBehaviour
{
    public int playerNum;
    public CharacterSelectManager characterManager;
    public CharacterSelectUI characterSelectUI;
    private ControlsManager controlsManager;
    protected GenericPlayer playerScript;
    private int characterIndex = 0;
    private bool characterSelected = false;

    void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
        characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
        characterSelectUI.UpdateSelected(characterSelected);
    }

    void Update()
    {
        if (!characterSelected)
        {
            // browse characters
            if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.LeftKey)))
            {
                characterIndex -= 1;
                if (characterIndex<0) characterIndex = 3;
                characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
            }
            else if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.RightKey)))
            {
                characterIndex += 1;
                if (characterIndex>3) characterIndex = 0;
                characterSelectUI.UpdateCharacterDisplayed(characterManager.GetCharacter(characterIndex));
            }

            // select character
            if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.PrimaryKey)))
            {
                // if character not taken
                if (characterManager.SelectCharacter(characterIndex))
                {
                    characterSelected = true;
                    characterSelectUI.UpdateSelected(true);
                }
            }
        }
        else
        {
            // undo selection
            if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.SecondaryKey)))
            {
                characterSelected = false;
                characterManager.UnSelectCharacter(characterIndex);
                characterSelectUI.UpdateSelected(false);
            }

        }
    }
}
