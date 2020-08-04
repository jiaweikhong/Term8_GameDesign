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
    private float min;
    private float sec;
    private float msec;

    [SerializeField]
    public bool inBattle = false;

    void Start()
    {
        screensTransitionManager = FindObjectOfType<ScreensTransitionManager>();
        SetCountDown(min, sec, msec);
        invisPlatform.SetActive(false);
    }

    public void StartMatch()
    {
        for (int i = 0; i< playerUIs.Length; i++)
        {
            playerUIs[i].UpdatePlayer(playerStatsList[i]);
        }
        inBattle = true;
        NewMatch();
    }

    void Update()
    {        
        if (min==0 && sec==0 && (int) msec==0 && inBattle == true)
        {
            inBattle = false;
            screensTransitionManager.ToAfterMatch();
        }
        else if (inBattle == true)
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

    public void NewMatch()
    {
        min = 0;
        sec = 30;
        msec = 0;
    }
}
