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

    public int requiredPlayersToStart = 4;
    private int screenNum = 0;
    [SerializeField]
    private int readyPlayersNum = 0;

    private AudioSource audioSrc;
    public AudioClip toSelectPlaySFX;

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
        audioSrc = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (screenNum == 1)
        {
            if (readyPlayersNum == requiredPlayersToStart)
            {
                screenNum += 1;
                readyPlayersNum = 0;
                StartCoroutine(ToBrewingPhase());
            }
        }
        else if (screenNum == 2)
        {
            if (readyPlayersNum == requiredPlayersToStart)
            {
                screenNum += 1;
                readyPlayersNum = 0;
                StartCoroutine(ToGamePlay());
            }
        }
        
        else if (screenNum == 4)
        {
            if (readyPlayersNum == requiredPlayersToStart)
            {
                screenNum = 2;
                readyPlayersNum = 0;
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
        brewingPhaseCanvas.SetActive(false);
        controlsManager.SwitchAllControllersToCharacterMode();
    }

    public void ToAfterMatch() {
        screenNum += 1;
        afterMatchCanvas.SetActive(true);
        controlsManager.SwitchAllControllersToUIMode();
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


}
