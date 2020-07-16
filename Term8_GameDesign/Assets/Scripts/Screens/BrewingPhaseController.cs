using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class BrewingPhaseController : MonoBehaviour
{

    public int playerNum;
    public PlayerStats playerStats;
    public BrewingPhaseUI brewingPhaseUI;
    private ControlsManager controlsManager;
    private BrewingPhaseManager brewingManager;
    private ScreensTransitionManager screensTransitionManager;
    private int secondaryQty;
    private int specialQty;
    private int weets;
    private int specialIndex = 0;
    private int selectionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // get reference and display default
        controlsManager = FindObjectOfType<ControlsManager>();
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        brewingManager = FindObjectOfType<BrewingPhaseManager>();

        secondaryQty = playerStats.SecondaryPotionQty;
        specialQty = playerStats.SpecialPotionQty;
        weets = playerStats.Weets;
        brewingPhaseUI.UpdatePlayer(playerStats);
        brewingPhaseUI.UpdateSelectionBox(selectionIndex);
        brewingPhaseUI.UpdateSpecialPotion(brewingManager.GetSpecialPotion(specialIndex), weets);
    }

    // Update is called once per frame
    void Update()
    {
        // handle selection field {0: secondary, 1: special, 2: confirm, 3: ready}
        if (selectionIndex < 3)
        {
            //if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.UpKey)))
            if (controlsManager.CheckUpSelect(playerNum))
            {
                controlsManager.LockNavigation(playerNum);
                selectionIndex -= 1;
                if (selectionIndex < 0) selectionIndex = 2;
                brewingPhaseUI.UpdateSelectionBox(selectionIndex);
            }
            //else if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.DownKey)))
            else if (controlsManager.CheckDownSelect(playerNum))
            {
                controlsManager.LockNavigation(playerNum);
                selectionIndex += 1;
                if (selectionIndex > 2) selectionIndex = 0;
                brewingPhaseUI.UpdateSelectionBox(selectionIndex);
            }

            // secondary potion
            if (selectionIndex == 0)
            {
                int secondaryCost = 20;
                if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.PrimaryKey))) // buy 1
                {
                    if (weets - secondaryCost >= 0) // check enough money
                    {
                        secondaryQty += 1;
                        weets -= secondaryCost;
                        brewingPhaseUI.UpdateSecondaryQty(secondaryQty, weets);
                    }
                }
                else if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.SecondaryKey))) // unbuy 1
                {
                    if (secondaryQty > 0) // check enough qty
                    {
                        secondaryQty -= 1;
                        weets += secondaryCost;
                        brewingPhaseUI.UpdateSecondaryQty(secondaryQty, weets);
                    }
                }
            }
            // special potion
            else if (selectionIndex == 1)
            {
                int specialCost = brewingManager.GetSpecialPotion(specialIndex).Cost;
                int numSpecialPotions = brewingManager.NumSpecialPotions();

                if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.PrimaryKey))) // buy 1
                {
                    if (weets - specialCost >= 0) // check enough money
                    {
                        specialQty += 1;
                        weets -= specialCost;
                        brewingPhaseUI.UpdateSpecialQty(specialQty, weets);
                    }
                }
                else if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.SecondaryKey))) // unbuy 1
                {
                    if (specialQty > 0) // check enough qty
                    {
                        specialQty -= 1;
                        weets += specialCost;
                        brewingPhaseUI.UpdateSpecialQty(specialQty, weets);
                    }
                }
                //else if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.LeftKey))) // browse special potion
                else if(controlsManager.CheckLeftSelect(playerNum))
                {
                    controlsManager.LockNavigation(playerNum);
                    // refund money
                    if (specialQty > 0)
                    {
                        weets += specialQty * specialCost;
                        specialQty = 0;
                    }
                    // change index and displayed
                    specialIndex -= 1;
                    if (specialIndex < 0) specialIndex = numSpecialPotions - 1;
                    brewingPhaseUI.UpdateSpecialPotion(brewingManager.GetSpecialPotion(specialIndex), weets);
                }
                //else if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.RightKey))) // browse special potion
                else if (controlsManager.CheckRightSelect(playerNum))
                {
                    controlsManager.LockNavigation(playerNum);
                    // refund money
                    if (specialQty > 0)
                    {
                        weets += specialQty * specialCost;
                        specialQty = 0;
                    }
                    // change index and displayed
                    specialIndex += 1;
                    if (specialIndex > numSpecialPotions - 1) specialIndex = 0;
                    brewingPhaseUI.UpdateSpecialPotion(brewingManager.GetSpecialPotion(specialIndex), weets);
                }
            }
            // confirm button
            else if (selectionIndex == 2)
            {
                if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.PrimaryKey)))
                {
                    selectionIndex = 3;
                    playerStats.UpdateBrew(weets, secondaryQty, specialQty, brewingManager.GetSpecialPotion(specialIndex));
                    brewingPhaseUI.UpdateSelected(true);
                    screensTransitionManager.ReadyPlayer(true);
                }
            }
        }
        else
        {
            // undo ready
            if (Input.GetKeyDown(controlsManager.GetKey(playerNum, ControlKeys.SecondaryKey)))
            {
                selectionIndex = 2;
                screensTransitionManager.ReadyPlayer(false);
            }
        }

        // allows joycon to navigate when user returns joystick to neutral
        for (int i = 0; i < 4; i++)
        {
            controlsManager.UnlockNavigation(i);
        }
    }
}
