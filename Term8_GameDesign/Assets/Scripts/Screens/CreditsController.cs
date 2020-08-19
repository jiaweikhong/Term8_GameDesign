using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField]
    private ScreensTransitionManager screensTransitionManager;
    private AudioSource audioSrc;
    public AudioClip cancelSFX;

    private void Awake()
    {
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
