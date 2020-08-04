using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    private ScreensTransitionManager screensTransitionManager;
    private AudioSource audioSrc;
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
    
    private IEnumerator ToTitle()
    {
        audioSrc.PlayOneShot(cancelSFX);
        yield return new WaitForSeconds(0.5f);
        screensTransitionManager.ToTitle();
    }
}
