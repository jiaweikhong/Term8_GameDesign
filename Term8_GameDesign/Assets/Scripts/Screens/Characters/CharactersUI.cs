using UnityEngine;
using UnityEngine.UI;

public class CharactersUI : MonoBehaviour
{
    [SerializeField] 
    private Image sprite;
    [SerializeField]
    private Text characterName;
    [SerializeField]
    private Text title;
    [SerializeField] 
    private Text backstory;
    

    public void UpdateCharacter(CharacterData data)
    {
        sprite.sprite = data.Sprite;
        characterName.text = data.CharacterName;
        title.text = data.Title;
        backstory.text = data.Backstory;
    }
}
