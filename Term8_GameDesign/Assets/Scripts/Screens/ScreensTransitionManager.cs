using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ScreensTransitionManager : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject characterSelectCanvas;
    public GameObject brewingPhaseCanvas;
    private ControlsManager controlsManager;
    private int screenNum = 0;
    private int readyPlayersNum = 0;

    public void Start()
    {
        // get references and set active/inactive pages 
        controlsManager = FindObjectOfType<ControlsManager>();
        titleCanvas.SetActive(true);
        characterSelectCanvas.SetActive(false);
        brewingPhaseCanvas.SetActive(false);
    }

    public void Update()
    {
        if (screenNum == 0)
        {
            // check player controls
            // TODO: separate script if need to handle selection of other menu buttons in title screen
            if (Input.GetKeyDown(controlsManager.GetKey(0, ControlKeys.PrimaryKey)))
            {
                screenNum += 1;
                // set active/inactive relevant pages 
                characterSelectCanvas.SetActive(true);
                titleCanvas.SetActive(false);
            }
        }
        else if (screenNum == 1)
        {
            if (readyPlayersNum == 4)
            {
                screenNum += 1;
                readyPlayersNum = 0;
                StartCoroutine(toBrewingPhase());
            }
        }
        else if (screenNum == 2)
        {
            if (readyPlayersNum == 4)
            {
                //toPlay();
                StartCoroutine(toGamePlay());
            }
        }

    }

    public void ReadyPlayer(bool ready)
    {
        readyPlayersNum += (ready) ? 1 : -1;
    }

    private IEnumerator toBrewingPhase()
    {
        yield return new WaitForSeconds(1f);
        brewingPhaseCanvas.SetActive(true);
        characterSelectCanvas.SetActive(false);
    }

    private IEnumerator toGamePlay()
    {
        yield return new WaitForSeconds(1f);
        titleCanvas.SetActive(true);
        brewingPhaseCanvas.SetActive(false);
    }
}
