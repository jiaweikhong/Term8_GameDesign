using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] 
    private Text characterName;
    [SerializeField] 
    private Image sprite;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Image primarySprite;
    [SerializeField]
    private Text primaryName;
    [SerializeField]
    private Image secondarySprite;
    [SerializeField]
    private Text secondaryName;
    [SerializeField]
    private GameObject arrows;
    [SerializeField]
    private Text Confirm;
    [SerializeField]
    private GameObject confirmBorder;

    public void UpdateCharacterDisplayed(CharacterData data)
    {
        characterName.text = data.CharacterName;
        sprite.sprite = data.Sprite;
        title.text = data.Title;
        primarySprite.sprite = data.PrimarySprite;
        primaryName.text = data.PrimaryName;
        secondarySprite.sprite = data.SecondarySprite;
        secondaryName.text = data.SecondaryName;
    }

    public void UpdateSelected(bool isSelected)
    {
        arrows.SetActive(!isSelected);
        confirmBorder.SetActive(!isSelected);
        Confirm.text = (isSelected) ? "READY >" : "CONFIRM";
    }
}
