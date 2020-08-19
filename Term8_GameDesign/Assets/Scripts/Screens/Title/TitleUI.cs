using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    private GameObject playBox;
    [SerializeField]
    private GameObject instructionsBox;
    [SerializeField]
    private GameObject controlsBox;
    [SerializeField]
    private GameObject charactersBox;
    [SerializeField]
    private GameObject creditsBox;
    [SerializeField]
    private GameObject quitGameBox;

    public void UpdateSelectionBox(int index)
    {
        playBox.SetActive(index == 0);
        instructionsBox.SetActive(index == 1);
        controlsBox.SetActive(index == 2);
        charactersBox.SetActive(index == 3);
        creditsBox.SetActive(index == 4);
        quitGameBox.SetActive(index == 5);
    }
}
