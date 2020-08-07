using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class ScreensTransitionManager : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject instructionsCanvas;
    public GameObject controlsCanvas;
    public GameObject charactersCanvas;
    public GameObject characterSelectCanvas;
    public GameObject brewingPhaseCanvas;
    public GameObject afterMatchCanvas;
    public GameObject gameOverlayCanvas;
    public GameObject podiumOverlayCanvas;
    public GameObject podium;
    public ControlsManager controlsManager;
    private GameOverlayController gameOverlayController;

    public SpawnPickups spawnPickupsScript;

    public int requiredPlayersToStart = 4;
    private bool isFirstGame = true;
    private bool inWalkthrough = false;
    private int screenNum = 0;
    [SerializeField]
    private int readyPlayersNum = 0;
    private int matchNum = 0;

    // for SFX
    private AudioSource audioSrc;
    public AudioClip toSelectPlaySFX;
    public AudioClip roundEndSFX;

    // for BGM
    public AudioManager audioManager;

    // for return to title
    public delegate void ReturnToTitle();
    public event ReturnToTitle OnReturnToTitle;

    public int GetScreenNum()
    {
        return screenNum;
    }

    public bool InWalkthrough()
    {
        return inWalkthrough;
    }

    public void EndWalkthrough()
    {
        inWalkthrough = false;
        isFirstGame = false;
        ToCharacterSelect();
    }

    private void Start()
    {
        // set active/inactive pages 
        titleCanvas.SetActive(true);
        charactersCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        characterSelectCanvas.SetActive(false);
        brewingPhaseCanvas.SetActive(false);
        afterMatchCanvas.SetActive(false);
        gameOverlayCanvas.SetActive(false);
        podiumOverlayCanvas.SetActive(false);

        podium.SetActive(false);
        gameOverlayController = gameOverlayCanvas.GetComponent<GameOverlayController>();
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (screenNum == 1) // character select
        {
            if (readyPlayersNum == requiredPlayersToStart)
            {
                screenNum += 1;
                readyPlayersNum = 0;
                StartCoroutine(ToBrewingPhase());
            }
        }
        else if (screenNum == 2) // brewing phase
        {
            if (readyPlayersNum == requiredPlayersToStart)
            {
                screenNum += 1;
                readyPlayersNum = 4;
                StartCoroutine(ToGamePlay());
            }
        }
        else if (screenNum == 4) // after match
        {
            if (readyPlayersNum == requiredPlayersToStart)
            {
                screenNum = 2;
                readyPlayersNum = 0;
                StartCoroutine(ToBrewingPhase());
            }
        }
        else if (screenNum == 5) // podium
        {
            if (readyPlayersNum == requiredPlayersToStart)
            {
                screenNum = 0;
                readyPlayersNum = 0;
                matchNum = 0;
                
                // set active/inactive pages 
                titleCanvas.SetActive(true);
                charactersCanvas.SetActive(false);
                controlsCanvas.SetActive(false);
                characterSelectCanvas.SetActive(false);
                brewingPhaseCanvas.SetActive(false);
                afterMatchCanvas.SetActive(false);
                gameOverlayCanvas.SetActive(false);
                podiumOverlayCanvas.SetActive(false);
            }
        }
    }

    public void ReadyPlayer(bool ready)
    {
        readyPlayersNum += (ready) ? 1 : -1;
    }

    private IEnumerator SwitchControllers()
    {
        controlsManager.DisableActionMap(0);
        controlsManager.DisableActionMap(1);
        controlsManager.DisableActionMap(2);
        controlsManager.DisableActionMap(3);
        yield return new WaitForSeconds(2f);
        screenNum += 1;
        afterMatchCanvas.SetActive(true);
        controlsManager.SwitchAllControllersToUIMode();
    }

    private IEnumerator DestroyPickups(float delay)
    {
        // after screen has transited to match conclusion
        yield return new WaitForSeconds(delay);
        spawnPickupsScript.DestroyPickups();
    }

    public void ToTitle()
    {
        screenNum = 0;
        readyPlayersNum = 0;
        titleCanvas.SetActive(true);
        instructionsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        charactersCanvas.SetActive(false);
        characterSelectCanvas.SetActive(false);
        OnReturnToTitle?.Invoke();
        audioManager.ChangeTrack("toTitle");
    }

    public void ToInstructions()
    {
        screenNum = -1;
        instructionsCanvas.SetActive(true);
        titleCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
    }
    public void ToControls()
    {
        screenNum = -2;
        controlsCanvas.SetActive(true);
        titleCanvas.SetActive(false);
        instructionsCanvas.SetActive(false);
    }
    public void ToCharacters()
    {
        screenNum = -3;
        charactersCanvas.SetActive(true);
        titleCanvas.SetActive(false);
    }

    public void ToCharacterSelect()
    {
        if (isFirstGame)
        {
            inWalkthrough = true;
            ToInstructions();
        }
        else
        {
            screenNum = 1;
            characterSelectCanvas.SetActive(true);
            audioManager.ChangeTrack("characterSelect");
            titleCanvas.SetActive(false);
            controlsCanvas.SetActive(false);
        }
    }
    private IEnumerator ToBrewingPhase()
    {
        yield return new WaitForSeconds(1f);
        brewingPhaseCanvas.SetActive(true);
        characterSelectCanvas.SetActive(false);
        afterMatchCanvas.SetActive(false);

        audioManager.ChangeTrack("characterSelect");
    }
    private IEnumerator ToGamePlay()
    {
        yield return new WaitForSeconds(1f);
        gameOverlayCanvas.SetActive(true);
        gameOverlayController.StartMatch();
        brewingPhaseCanvas.SetActive(false);
        controlsManager.SwitchAllControllersToCharacterMode();
        matchNum += 1;

        audioManager.ChangeTrack("toGamePlay");

        // trigger start of spawnings
        spawnPickupsScript.StartSpawning(matchNum);
    }
    private IEnumerator ToFinal()
    {
        yield return new WaitForSeconds(1f);
        gameOverlayCanvas.SetActive(false);
        podium.SetActive(true);
        podiumOverlayCanvas.SetActive(true);
        audioManager.ChangeTrack("toAfterMatch");
    }
    public void ToAfterMatch()
    {
        spawnPickupsScript.StopSpawning();
        audioManager.audioSource.Stop();
        if (screenNum == 3)
        {
            audioSrc.PlayOneShot(roundEndSFX);
            OnNewMatch.Invoke();
            
            if (matchNum <= 2)
            {
                StartCoroutine(SwitchControllers());
                StartCoroutine(DestroyPickups(2.1f));
            }
            else
            {
                screenNum = 5;
                readyPlayersNum = 0;
                StartCoroutine(ToFinal());
                StartCoroutine(DestroyPickups(1f));
            }
        }
    }

    public delegate void NewMatch();
    public event NewMatch OnNewMatch;
}
