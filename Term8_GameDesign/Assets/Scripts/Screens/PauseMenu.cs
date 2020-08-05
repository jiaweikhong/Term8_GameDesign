using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public ControlsManager controlsManager;
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

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    private void Start()
    {
        pauseMenuUI.SetActive(false);
        controlsCanvas.SetActive(false);
    }

    private void Update()
    {
        if (isGamePaused)
        {
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

    public void Resume()
    {
        StartCoroutine(SwitchToCharacterActionMaps());
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    IEnumerator SwitchToCharacterActionMaps()
    {
        yield return new WaitForSeconds(0.1f);
        controlsManager.SwitchAllControllersToCharacterMode();
    }

    void Pause()
    {
        audioSrc.PlayOneShot(selectSFX);
        controlsManager.SwitchAllControllersToUIMode();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void TogglePause()
    {
        if (isGamePaused)
        {
            Resume();
        }
        else if (!isGamePaused)
        {
            Pause();
        }
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
    }

    public void LoadControls()
    {
        Debug.Log("Loading Controls...");
        controlsCanvas.SetActive(true);
    }

    public void HideControls()
    {
        Debug.Log("Hiding controls...");
        controlsCanvas.SetActive(false);
    }

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
