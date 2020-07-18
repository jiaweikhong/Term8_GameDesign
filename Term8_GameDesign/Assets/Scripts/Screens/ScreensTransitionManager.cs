using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScreensTransitionManager : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject characterSelectCanvas;
    public GameObject brewingPhaseCanvas;
    public ControlsManager controlsManager;

    public int requiredPlayersToStart = 4;
    private int screenNum = 0;
    [SerializeField]
    private int readyPlayersNum = 0;

    public int GetScreenNum()
    {
        return screenNum;
    }

    public void Start()
    {
        // set active/inactive pages 
        titleCanvas.SetActive(true);
        characterSelectCanvas.SetActive(false);
        brewingPhaseCanvas.SetActive(false);
    }

    public void Update()
    {
        if (screenNum == 1)
        {
            if (readyPlayersNum == requiredPlayersToStart)
            {
                screenNum += 1;
                readyPlayersNum = 0;
                StartCoroutine(toBrewingPhase());
            }
        }
        else if (screenNum == 2)
        {
            if (readyPlayersNum == requiredPlayersToStart)
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
        //titleCanvas.SetActive(true);
        brewingPhaseCanvas.SetActive(false);
        controlsManager.SwitchAllControllersToCharacterMode();
        SceneManager.LoadScene(1);
    }

    public void onSelectPlay()
    {
        if (screenNum == 0)
        {
            Debug.Log("screen trans manager select input");

            // check player controls
            // TODO: separate script if need to handle selection of other menu buttons in title screen
            screenNum += 1;
            // set active/inactive relevant pages 
            characterSelectCanvas.SetActive(true);
            titleCanvas.SetActive(false);
        }
    }


}
