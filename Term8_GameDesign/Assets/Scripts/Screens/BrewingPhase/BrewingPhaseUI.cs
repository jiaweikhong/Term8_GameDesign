using UnityEngine;
using UnityEngine.UI;

public class BrewingPhaseUI : MonoBehaviour
{
    [SerializeField] 
    private Image sprite;
    [SerializeField]
    private Image primarySprite;
    [SerializeField]
    private Text primaryName;
    [SerializeField] 
    private Text weets;
    [SerializeField]
    private Image secondarySprite;
    [SerializeField]
    private Text secondaryName;
    [SerializeField]
    private Text secondaryQty;
    [SerializeField]
    private GameObject arrows;
    [SerializeField]
    private Image specialSprite;
    [SerializeField]
    private Text specialName;
    [SerializeField]
    private Text specialQty;
    [SerializeField]
    private Text specialCost;
    [SerializeField]
    private Text Confirm;
    [SerializeField]
    private GameObject confirmBorder;
    [SerializeField]
    private GameObject secondaryBox;
    [SerializeField]
    private GameObject specialBox;
    [SerializeField]
    private GameObject confirmBox;


    public void UpdatePlayer(PlayerStats data)
    {
        CharacterData character = data.CharacterData;
        sprite.sprite = character.Sprite;
        primarySprite.sprite = character.PrimarySprite;
        primaryName.text = character.PrimaryName;
        secondarySprite.sprite = character.SecondarySprite;
        secondaryName.text = character.SecondaryName;

        //Debug.Log(data.Weets.ToString() + " w");
        weets.text = data.Weets.ToString() + " w";
        //weets.text = "hahha";
        secondaryQty.text = data.SecondaryPotionQty.ToString();
        // specialQty.text = data.SpecialPotionQty.ToString();
    }

    public void UpdateSpecialPotion(SpecialPotionData data, int value)
    {
        specialSprite.sprite = data.Sprite;
        specialName.text = data.SpecialName;
        specialCost.text = data.Cost.ToString() + " w";
        specialQty.text = "0";

        weets.text = value.ToString() + " w";
        //weets.text = "update special potion";
    }
    public void UpdateSecondaryQty(int qty, int value)
    {
        secondaryQty.text = qty.ToString();
        weets.text = value.ToString() + " w";
        //weets.text = "update sec qty";

    }
    public void UpdateSpecialQty(int qty, int value)
    {
        specialQty.text = qty.ToString();
        weets.text = value.ToString() + " w";
        //weets.text = "updatespecial qty";

    }
    public void UpdateSelectionBox(int index)
    {
        secondaryBox.SetActive(index == 0);
        specialBox.SetActive(index == 1);
        confirmBox.SetActive(index == 2);
    }

    public void UpdateSelected(bool isSelected)
    {
        // select/unselect response
        arrows.SetActive(!isSelected);
        confirmBox.SetActive(!isSelected);
        confirmBorder.SetActive(!isSelected);
        Confirm.text = (isSelected) ? "READY >" : "BREW";
    }
}
