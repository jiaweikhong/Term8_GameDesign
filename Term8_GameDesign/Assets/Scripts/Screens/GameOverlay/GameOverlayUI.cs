using UnityEngine;
using UnityEngine.UI;

public class GameOverlayUI : MonoBehaviour
{
    [SerializeField] 
    private Image sprite;
    [SerializeField] 
    private Text characterName;
    [SerializeField] 
    private Image secondarySprite;
    [SerializeField] 
    private Text secondaryQty;
    [SerializeField] 
    private Image specialSprite;
    [SerializeField] 
    private Text specialQty;
    [SerializeField] 
    private GameObject heart1;
    [SerializeField] 
    private GameObject heart2;
    [SerializeField] 
    private GameObject heart3;

    public void UpdatePlayer(PlayerStats data)
    {
        CharacterData character = data.CharacterData;
        sprite.sprite = character.Sprite;
        characterName.text = character.CharacterName;
        secondarySprite.sprite = character.SecondarySprite;
        secondaryQty.text = data.SecondaryPotionQty.ToString();
        specialSprite.sprite = data.SpecialPotion.Sprite;
        specialQty.text = data.SpecialPotionQty.ToString();
        UpdateNumHearts(data.PlayerHealth);
    }

    public void UpdateSecondaryQty(int secQty)
    {
        secondaryQty.text = secQty.ToString();
    }
    public void UpdateSpecialQty(int specQty)
    {
        specialQty.text = specQty.ToString();
    }
    public void UpdateNumHearts(int numHearts)
    {
        heart1.SetActive(numHearts >= 1);
        heart2.SetActive(numHearts >= 2);
        heart3.SetActive(numHearts == 3);
    }
}
