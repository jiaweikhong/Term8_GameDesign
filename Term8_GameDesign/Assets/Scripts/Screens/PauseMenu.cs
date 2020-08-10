using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField]
    private ControlsManager controlsManager;
    [SerializeField]
    private ScreensTransitionManager screensTransitionManager;
    [SerializeField]
    private GameManager gameManager;
    public GameObject pauseMenuUI;
    private int index = 0;      // 0 to 2
    private Vector2 navigateVector = Vector2.zero;
    private AudioSource audioSrc;
    public AudioClip navigateSFX;
    public AudioClip selectSFX;
    public AudioClip cancelSFX;
    public GameObject resumeButton;
    public Image resumeButtonImage;
    public GameObject controlsButton;
    public Image controlsButtonImage;
    public GameObject mainMenuButton;
    public Image mainMenuButtonImage;
    public GameObject controlsCanvas;
    private bool controlsCanvasOpen = false;
    [SerializeField]
    private bool inPodiumScene = false;

    public void SetInPodiumScene(bool setBool)
    {
        inPodiumScene = setBool;
    }

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        controlsCanvas.SetActive(false);
        gameManager.OnPodiumSceneEvent += SetInPodiumScene;
    }

    private void Update()
    {
        if (isGamePaused)
        {
            // Handles switching of colors of buttons
            switch (index)
            {
                case 0:
                    resumeButtonImage.color = new Color32(250, 140, 140, 255);
                    mainMenuButtonImage.color = new Color32(224, 102, 102, 255);
                    controlsButtonImage.color = new Color32(224, 102, 102, 255);
                    break;
                case 1:
                    controlsButtonImage.color = new Color32(250, 140, 140, 255);
                    resumeButtonImage.color = new Color32(224, 102, 102, 255);
                    mainMenuButtonImage.color = new Color32(224, 102, 102, 255);
                    break;
                case 2:
                    mainMenuButtonImage.color = new Color32(250, 140, 140, 255);
                    resumeButtonImage.color = new Color32(224, 102, 102, 255);
                    controlsButtonImage.color = new Color32(224, 102, 102, 255);
                    break;
            }
        }
    }

    // Resumes the game
    public void Resume()
    {
        StartCoroutine(SwitchToCharacterActionMaps());
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    // Switches all characters' actionmaps to CharacterActions after an insignificant delay.
    IEnumerator SwitchToCharacterActionMaps()
    {
        yield return new WaitForSeconds(0.1f);
        controlsManager.SwitchAllControllersToCharacterMode();
    }

    // Pauses the game
    void Pause()
    {
        audioSrc.PlayOneShot(selectSFX);
        controlsManager.SwitchAllControllersToUIMode();
        index = 0;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    // Toggles between pause and unpause
    public void TogglePause()
    {
        if (inPodiumScene)
        {
            // Do not open up a pause menu if player is in podium scene
            return;
        }
        if (isGamePaused)
        {
            Resume();
        }
        else if (!isGamePaused)
        {
            Pause();
        }
    }

    // Returns the game to the title screen
    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        screensTransitionManager.ToTitle();
    }

    // Shows the controls screen
    public void LoadControls()
    {
        controlsCanvas.SetActive(true);
    }

    // Hides the controls screen
    public void HideControls()
    {
        controlsCanvas.SetActive(false);
    }

    // Handles UI navigation
    public void NavigateInput(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy && context.performed && !controlsCanvasOpen)
        {
            navigateVector = context.ReadValue<Vector2>();
            // navigate down
            if (navigateVector.y < -0.5f && index >= 0 && index < 2)
            {
                index += 1;
                audioSrc.PlayOneShot(navigateSFX);
            }
            // navigate up
            else if (navigateVector.y > 0.5f && index > 0 && index <= 2)
            {
                index -= 1;
                audioSrc.PlayOneShot(navigateSFX);
            }
        }
    }

    // Handles UI Select
    public void Select()
    {
        switch (index)
        {
            case 0:
                // resume
                audioSrc.PlayOneShot(selectSFX);
                TogglePause();
                index = 0;
                break;
            case 1:
                // controls
                if (!controlsCanvasOpen)
                {
                    controlsCanvasOpen = true;
                    audioSrc.PlayOneShot(selectSFX);
                    LoadControls();
                }
                break;
            case 2:
                // return to main menu
                audioSrc.PlayOneShot(selectSFX);
                LoadMenu();
                //index = 0;
                break;
        }
    }

    // Handles UI Cancel
    public void CancelInput()
    {
        switch (index)
        {
            case 1:
                // controls
                if (controlsCanvasOpen)
                {
                    controlsCanvasOpen = false;
                    audioSrc.PlayOneShot(cancelSFX);
                    HideControls();
                }
                break;
        }
    }
}
