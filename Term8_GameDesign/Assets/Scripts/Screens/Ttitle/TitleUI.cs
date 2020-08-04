using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject playBox;
    [SerializeField]
    private GameObject charactersBox;
    [SerializeField]
    private GameObject instructionsBox;

    public void UpdateSelectionBox(int index)
    {
        playBox.SetActive(index==0);
        charactersBox.SetActive(index==1);
        instructionsBox.SetActive(index==2);
    }
}
