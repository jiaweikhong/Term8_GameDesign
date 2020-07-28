using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverlayController : MonoBehaviour
{
    public Text minCountdown;
    public Text msecCountdown;
    public GameObject invisPlatform;
    public PlayerStats[] playerStatsList;
    public GameOverlayUI[] playerUIs;
    private ScreensTransitionManager screensTransitionManager;
    private bool isPaused;
    // pauseOverlayCanvas
    private float min = 0;
    private float sec = 3;
    private float msec = 0;

    void Start()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        SetCountDown(min, sec, msec);
        for (int i = 0; i< playerUIs.Length; i++)
        {
            playerUIs[i].UpdatePlayer(playerStatsList[i]);
        }
        invisPlatform.SetActive(false);
    }

    void Update()
    {        
        if (min==0 && sec==0 && (int) msec==0)
        {
            screensTransitionManager.ToAfterMatch();
        }
        else
        {
            // if button pressed and ispaused
            
            if(msec <= 0){
                if(sec <= 0){
                    min--;
                    sec = 59;
                }
                else if(sec >= 0){
                    sec--;
                }
                    
                    msec = 100;
                }
            msec -= Time.deltaTime * 100;
            SetCountDown(min, sec, msec);
        }
        
    }

    private void SetCountDown(float minute, float second, float milisecond)
    {
        minCountdown.text = min.ToString() + ":" + second.ToString("00");
        msecCountdown.text = ((int) milisecond).ToString("00");
    }
}
