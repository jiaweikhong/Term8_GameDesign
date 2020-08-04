using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
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
        StartCoroutine(ToTitle());
    }
    public void SubmitInput()
    {
        if (screensTransitionManager.InWalkthrough())
        {
            StartCoroutine(ToControls());
        }
    }
    
    private IEnumerator ToTitle()
    {
        audioSrc.PlayOneShot(cancelSFX);
        yield return new WaitForSeconds(0.5f);
        screensTransitionManager.ToTitle();
    }
    private IEnumerator ToControls()
    {
        audioSrc.PlayOneShot(selectSFX);
        yield return new WaitForSeconds(0.5f);
        screensTransitionManager.ToControls();
    }
}
