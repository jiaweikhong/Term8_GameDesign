using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsController : MonoBehaviour
{
    private ScreensTransitionManager screensTransitionManager;
    private AudioSource audioSrc;
    public AudioClip selectSFX;
    public AudioClip cancelSFX;

    void Start()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        audioSrc = GetComponent<AudioSource>();
    }

    public void CancelInput()
    {
        StartCoroutine(Back(screensTransitionManager.InWalkthrough()));
    }

    public void SubmitInput()
    {
        if (screensTransitionManager.InWalkthrough())
        {
            StartCoroutine(ToCharacterSelect());
        }
    }
    
    private IEnumerator Back(bool inWalkthrough)
    {
        audioSrc.PlayOneShot(cancelSFX);
        yield return new WaitForSeconds(0.5f);
        if (inWalkthrough)
        {
            screensTransitionManager.ToInstructions();
        }
        else
        {
            screensTransitionManager.ToTitle();
        }
    }
    private IEnumerator ToCharacterSelect()
    {
        audioSrc.PlayOneShot(selectSFX);
        yield return new WaitForSeconds(0.5f);
        screensTransitionManager.EndWalkthrough();
    }
}
