using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterMatchController : MonoBehaviour
{
    public int playerNum;
    public PlayerStats playerStats;
    public AfterMatchUI afterMatchUI;
    private ScreensTransitionManager screensTransitionManager;
    private AfterMatchManager afterMatchManager;
    private bool playerReady = false;
    private AudioSource audioSrc;
    public AudioClip selectSFX;
    public AudioClip errorSFX;
    public AudioClip cancelSFX;

    // Start is called before the first frame update
    void Start()
    {
        // get reference and display default
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        afterMatchManager = FindObjectOfType<AfterMatchManager>();
        audioSrc = GetComponent<AudioSource>();
        afterMatchUI.UpdatePlayer(playerStats);
        afterMatchUI.UpdateRank(afterMatchManager.getPlayerRank(playerNum));

        screensTransitionManager.OnNewMatch += NewMatch;
    }

    public void SubmitInput()
    {
        if (gameObject.activeInHierarchy && !playerReady)
        {
            audioSrc.PlayOneShot(selectSFX);
            playerReady = true;
            afterMatchUI.UpdateSelected(true);
            screensTransitionManager.ReadyPlayer(true);
        }
        else
        {
            audioSrc.PlayOneShot(errorSFX);
        }
    }
    public void CancelInput()
    {
        if (gameObject.activeInHierarchy && playerReady)
        {
            // undo selection
            audioSrc.PlayOneShot(cancelSFX);
            playerReady = false;
            afterMatchUI.UpdateSelected(false);
            screensTransitionManager.ReadyPlayer(false);
        }
        else if (gameObject.activeInHierarchy && !playerReady)
        {
            audioSrc.PlayOneShot(errorSFX);
        }
    }

    public void NewMatch()
    {
        playerReady = false;
        afterMatchUI.UpdateSelected(false);
        afterMatchUI.UpdateRank(afterMatchManager.getPlayerRank(playerNum));
    }
}
