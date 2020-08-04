using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;

public class CharactersController : MonoBehaviour
{
    public CharacterData[] characterDataList;
    public CharactersUI charactersUI;
    private ScreensTransitionManager screensTransitionManager;
    private int characterIndex = 0;
    private Vector2 navigateVector = new Vector2(0, 0);
    private AudioSource audioSrc;
    public AudioClip navigateSFX;
    public AudioClip cancelSFX;
    void Start()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        audioSrc = GetComponent<AudioSource>();
    }
    public void CancelInput()
    {
        StartCoroutine(ToTitle());
    }

    public void NavigateInput(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy && context.performed)
        {
            // browse characters
            navigateVector = context.ReadValue<Vector2>();

            // navigate LEFT
            if (navigateVector.x < -0.5f)
            {
                characterIndex -= 1;
                characterIndex = (characterIndex<0) ? 3 : characterIndex;
            }

            else if (navigateVector.x > 0.5f)
            {
                characterIndex += 1;
                characterIndex = (characterIndex>3) ? 0 : characterIndex;
            }

            audioSrc.PlayOneShot(navigateSFX);
            charactersUI.UpdateCharacter(characterDataList[characterIndex]);
        }
    }

    private IEnumerator ToTitle()
    {
        audioSrc.PlayOneShot(cancelSFX);
        yield return new WaitForSeconds(0.5f);
        screensTransitionManager.ToTitle();
    }
}
