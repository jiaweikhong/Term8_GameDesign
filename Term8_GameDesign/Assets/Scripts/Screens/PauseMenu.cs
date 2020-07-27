using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
    public GameObject resumeButton;
    public GameObject controlsButton;
    public GameObject mainMenuButton;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    private void Start()
    {
        pauseMenuUI.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (isGamePaused)
        {
            switch (index)
            {
                case 0:
                    EventSystem.current.SetSelectedGameObject(resumeButton);
                    break;
                case 1:
                    EventSystem.current.SetSelectedGameObject(controlsButton);
                    break;
                case 2:
                    EventSystem.current.SetSelectedGameObject(mainMenuButton);
                    break;
                default:
                    EventSystem.current.SetSelectedGameObject(resumeButton);
                    break;
            }
        }


        // TODO: change the input method
        /*        if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (isGamePaused)
                    {
                        Resume();
                    } else
                    {
                        Pause();
                    }
                } */
    }

    void Resume()
    {
        controlsManager.SwitchAllControllersToCharacterMode();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
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
    }

    public void NavigateInput(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy && context.performed)
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
                audioSrc.PlayOneShot(selectSFX);
                break;
            case 2:
                // return to main menu
                audioSrc.PlayOneShot(selectSFX);
                index = 0;
                break;
        }
    }
}
