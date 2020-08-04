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
    public GameObject afterMatchCanvas;
    public GameObject gameOverlayCanvas;
    public ControlsManager controlsManager;
    private GameOverlayController gameOverlayController;

    public SpawnPickups spawnPickupsScript;

    public int requiredPlayersToStart = 4;
    private int screenNum = 0;
    [SerializeField]
    private int readyPlayersNum = 0;
    private int matchNum = 0;

    private AudioSource audioSrc;
    public AudioClip toSelectPlaySFX;
    public AudioClip roundEndSFX;

    public int GetScreenNum()
    {
        return screenNum;
    }

    // public void Awake()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }

    private void Start()
    {
        // set active/inactive pages 
        titleCanvas.SetActive(true);
        characterSelectCanvas.SetActive(false);
        brewingPhaseCanvas.SetActive(false);
        afterMatchCanvas.SetActive(false);
        gameOverlayCanvas.SetActive(false);
        gameOverlayController = gameOverlayCanvas.GetComponent<GameOverlayController>();
        audioSrc = GetComponent<AudioSource>();
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
                Debug.Log("going to brewing phase now");
                StartCoroutine(ToBrewingPhase());
            }
        }
    }


    public void ReadyPlayer(bool ready)
    {
        readyPlayersNum += (ready) ? 1 : -1;
    }

    private IEnumerator ToBrewingPhase()
    {
        yield return new WaitForSeconds(1f);
        brewingPhaseCanvas.SetActive(true);
        characterSelectCanvas.SetActive(false);
        afterMatchCanvas.SetActive(false);
    }

    private IEnumerator ToGamePlay()
    {
        yield return new WaitForSeconds(1f);
        gameOverlayCanvas.SetActive(true);
        gameOverlayController.StartMatch();
        brewingPhaseCanvas.SetActive(false);
        controlsManager.SwitchAllControllersToCharacterMode();
        matchNum += 1;

        // trigger start of spawnings
        spawnPickupsScript.StartSpawning();
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

    private IEnumerator DestroyPickups()
    {
        // after screen has transited to match conclusion
        yield return new WaitForSeconds(2.1f);
        spawnPickupsScript.DestroyPickups();
    }

    public void ToAfterMatch()
    {
        spawnPickupsScript.StopSpawning();
        if (screenNum == 3)
        {
            audioSrc.PlayOneShot(roundEndSFX);
            OnNewMatch?.Invoke();
            
            if (matchNum <= 2)
            {
                StartCoroutine(SwitchControllers());
                StartCoroutine(DestroyPickups());
            }
            else
            {
                Debug.Log("Gabriel's final screen");
                // final match ui
            }
        }
    }

    public void onSelectPlay()
    {
        if (screenNum == 0)
        {
            audioSrc.PlayOneShot(toSelectPlaySFX);
            screenNum += 1;
            characterSelectCanvas.SetActive(true);
            titleCanvas.SetActive(false);
        }
    }

    public delegate void NewMatch();
    public event NewMatch OnNewMatch;
}
