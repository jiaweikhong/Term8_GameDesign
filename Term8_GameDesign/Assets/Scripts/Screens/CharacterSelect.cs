using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
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

    public void UpdateDisplayUI(CharacterData data)
    {
        characterName.text = data.CharacterName;
        sprite.sprite = data.Sprite;
        title.text = data.Title;
        primarySprite.sprite = data.PrimarySprite;
        primaryName.text = data.PrimaryName;
        secondarySprite.sprite = data.SecondarySprite;
        secondaryName.text = data.SecondaryName;
    }
}
