using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumOverlayController : MonoBehaviour
{
    public int playerNum;
    public PlayerStats playerStats;
    public PodiumOverlayUI podiumOverlayUI;
    private ScreensTransitionManager screensTransitionManager;
    private AfterMatchManager afterMatchManager;
    private bool isReady = false;
    void Start()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        afterMatchManager = FindObjectOfType<AfterMatchManager>();

        afterMatchManager.OnUpdateRank += UpdateRank;
    }

    public void UpdateRank()
    {
        podiumOverlayUI.UpdatePlayer(playerStats);
        podiumOverlayUI.UpdateRank(afterMatchManager.getPlayerRank(playerNum));
    }

    public void UpdateReady()
    {
        isReady = !isReady;
        podiumOverlayUI.UpdateReady(isReady);
        screensTransitionManager.ReadyPlayer(isReady);
    }
}
