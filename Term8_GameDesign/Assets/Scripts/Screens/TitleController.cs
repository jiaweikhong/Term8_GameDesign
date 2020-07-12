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
        // get references and set active/inactive pages 
        controlsManager = FindObjectOfType<ControlsManager>();
        titleCanvas.SetActive(true);
        characterSelectCanvas.SetActive(false);
    }
    public void clickPlay()
    {
        // set active/inactive relevant pages 
        titleCanvas.SetActive(false);
        characterSelectCanvas.SetActive(true);
    }

    public void Update()
    {
        // check player controls
        // TODO: handle selection of other menu buttons
        if (Input.GetKeyDown(controlsManager.GetKey(0, ControlKeys.PrimaryKey)))
        {
            clickPlay();
        }
    }
}
