using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;

public class TitleController : MonoBehaviour
{
    public TitleUI titleUI;
    private int selectionIndex = 0;
    private ScreensTransitionManager screensTransitionManager;
    private Vector2 navigateVector = new Vector2(0, 0);
    private AudioSource audioSrc;
    public AudioClip navigateSFX;
    public AudioClip selectSFX;
    public AudioClip errorSFX;

    void Start()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        titleUI.UpdateSelectionBox(selectionIndex);
        audioSrc = GetComponent<AudioSource>();
    }

    public void SubmitInput()
    {
        StartCoroutine(NavigateTitle());
    }

    public void CancelInput()
    {
        audioSrc.PlayOneShot(errorSFX);
    }
    
    public void NavigateInput(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy && context.performed)
        {
            navigateVector = context.ReadValue<Vector2>();
            
            // navigate UP
            if (navigateVector.y > 0.5f && selectionIndex==0)
            {
                selectionIndex = 1;
            }
            // navigate DOWN
            if (navigateVector.y < -0.5f && selectionIndex!=0)
            {
                selectionIndex = 0;
            }

            // navigate LEFT
            if (navigateVector.x < -0.5f)
            {
                selectionIndex-=1;
                selectionIndex = (selectionIndex<0) ? 3 : selectionIndex;
            }
            
            // navigate RIGHT
            else if (navigateVector.x > 0.5f)
            {
                selectionIndex+=1;
                selectionIndex = (selectionIndex>3) ? 0 : selectionIndex;
            }
            
            titleUI.UpdateSelectionBox(selectionIndex);
            audioSrc.PlayOneShot(navigateSFX);
        }
    }

    private IEnumerator NavigateTitle()
    {
        audioSrc.PlayOneShot(selectSFX);
        yield return new WaitForSeconds(0.5f);
        if (selectionIndex==0)
        {
            screensTransitionManager.ToCharacterSelect();
        }
        else if (selectionIndex==1)
        {
            screensTransitionManager.ToInstructions();
        }
        else if (selectionIndex==2)
        {
            screensTransitionManager.ToControls();
        }
        else if (selectionIndex==3)
        {
            screensTransitionManager.ToCharacters();
        }
    }
}
