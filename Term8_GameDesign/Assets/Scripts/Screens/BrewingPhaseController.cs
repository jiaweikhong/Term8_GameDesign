using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;

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

    private Vector2 navigateVector = new Vector2(0, 0);
    private AudioSource audioSrc;
    public AudioClip navigateSFX;
    public AudioClip incrementPotionSFX;
    public AudioClip selectSFX;
    public AudioClip errorSFX;
    public AudioClip cancelSFX;

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
        audioSrc = GetComponent<AudioSource>();
    }

    public void NavigateInput(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy && context.performed)
        {
            // browse characters
            navigateVector = context.ReadValue<Vector2>();

            int specialCost = brewingManager.GetSpecialPotion(specialIndex).Cost;
            int numSpecialPotions = brewingManager.NumSpecialPotions();

            // navigate UP
            if (navigateVector.y > 0.5f)
            {
                if (selectionIndex < 3)
                {
                    audioSrc.PlayOneShot(navigateSFX);
                    selectionIndex -= 1;
                    if (selectionIndex < 0) selectionIndex = 2;
                    brewingPhaseUI.UpdateSelectionBox(selectionIndex);
                }

            }
            // navigate DOWN
            else if (navigateVector.y < -0.5f)
            {
                if (selectionIndex < 3)
                {
                    audioSrc.PlayOneShot(navigateSFX);
                    selectionIndex += 1;
                    if (selectionIndex > 2) selectionIndex = 0;
                    brewingPhaseUI.UpdateSelectionBox(selectionIndex);
                }
            }
            // navigate LEFT
            else if (navigateVector.x < -0.5f)
            {
                if (selectionIndex == 1)
                {
                    audioSrc.PlayOneShot(navigateSFX);
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
            }
            // navigate RIGHT
            else if (navigateVector.x > 0.5f)
            {
                if (selectionIndex == 1)
                {
                    audioSrc.PlayOneShot(navigateSFX);
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
        }
    }

    public void SubmitInput(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy && context.performed)
        {
            // buy 1 secondary potion
            if (selectionIndex == 0)
            {
                int secondaryCost = 20;
                if (weets - secondaryCost >= 0) // check enough money
                {
                    audioSrc.PlayOneShot(incrementPotionSFX);
                    secondaryQty += 1;
                    weets -= secondaryCost;
                    brewingPhaseUI.UpdateSecondaryQty(secondaryQty, weets);
                }
                else
                {
                    audioSrc.PlayOneShot(errorSFX);
                }
            }
            // buy 1 special potion
            else if (selectionIndex == 1)
            {
                int specialCost = brewingManager.GetSpecialPotion(specialIndex).Cost;
                //int numSpecialPotions = brewingManager.NumSpecialPotions();
                if (weets - specialCost >= 0) // check enough money
                {
                    audioSrc.PlayOneShot(incrementPotionSFX);
                    specialQty += 1;
                    weets -= specialCost;
                    brewingPhaseUI.UpdateSpecialQty(specialQty, weets);
                }
                else
                {
                    audioSrc.PlayOneShot(errorSFX);
                }
            }
            // confirm button
            else if (selectionIndex == 2)
            {
                audioSrc.PlayOneShot(selectSFX);
                selectionIndex = 3;
                playerStats.UpdateBrew(weets, secondaryQty, specialQty, brewingManager.GetSpecialPotion(specialIndex));
                brewingPhaseUI.UpdateSelected(true);
                screensTransitionManager.ReadyPlayer(true);
            }
        }
    }

    public void CancelInput(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy && context.performed)
        {
            // sell 1 secondary potion
            if (selectionIndex == 0)
            {
                int secondaryCost = 20;
                if (secondaryQty > 0) // check enough qty
                {
                    audioSrc.PlayOneShot(cancelSFX);
                    secondaryQty -= 1;
                    weets += secondaryCost;
                    brewingPhaseUI.UpdateSecondaryQty(secondaryQty, weets);
                }
                else
                {
                    audioSrc.PlayOneShot(errorSFX);
                }
            }
            // sell 1 special potion
            else if (selectionIndex == 1)
            {
                int specialCost = brewingManager.GetSpecialPotion(specialIndex).Cost;
                //int numSpecialPotions = brewingManager.NumSpecialPotions();

                if (specialQty > 0) // check enough qty
                {
                    audioSrc.PlayOneShot(cancelSFX);
                    specialQty -= 1;
                    weets += specialCost;
                    brewingPhaseUI.UpdateSpecialQty(specialQty, weets);
                }
                else
                {
                    audioSrc.PlayOneShot(errorSFX);
                }
            }
            // yet to confirm brew
            else if (selectionIndex == 2)
            {
                audioSrc.PlayOneShot(errorSFX);
            }
            // cancel readiness
            else if (selectionIndex == 3)
            {
                audioSrc.PlayOneShot(cancelSFX);
                selectionIndex = 2;
                brewingPhaseUI.UpdateSelected(false);
                screensTransitionManager.ReadyPlayer(false);
            }
        }
    }
}
