using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TitleController : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject characterSelectCanvas;
    private ControlsManager controlsManager;

    public void Start()
    {
        controlsManager = FindObjectOfType<ControlsManager>();
        titleCanvas.SetActive(true);
        characterSelectCanvas.SetActive(false);
    }
    public void clickPlay()
    {
        titleCanvas.SetActive(false);
        characterSelectCanvas.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(controlsManager.GetKey(0, ControlKeys.PrimaryKey)))
        {
            clickPlay();
        }
    }
}
