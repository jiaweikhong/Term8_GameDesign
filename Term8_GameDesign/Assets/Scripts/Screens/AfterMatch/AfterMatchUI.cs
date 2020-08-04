using UnityEngine;
using UnityEngine.UI;

public class AfterMatchUI : MonoBehaviour
{
    [SerializeField] 
    private Image sprite;
    [SerializeField] 
    private Text rank;
    [SerializeField] 
    private Text kills;
    [SerializeField] 
    private Text deaths;
    [SerializeField] 
    private Text weets;
    [SerializeField]
    private Text Confirm;
    [SerializeField]
    private GameObject confirmBorder;

    public void UpdatePlayer(PlayerStats data)
    {
        CharacterData character = data.CharacterData;
        sprite.sprite = character.Sprite;
        kills.text = data.PlayerKills.ToString();
        deaths.text = data.PlayerDeaths.ToString();
        weets.text = data.Weets.ToString();
    }
    public void UpdateRank(string playerRank)
    {
        rank.text = playerRank;
    }
    public void UpdateSelected(bool isSelected)
    {
        Debug.Log("called afterMatchUI UpdateSelected");
        // select/unselect response
        confirmBorder.SetActive(!isSelected);
        Confirm.text = (isSelected) ? "READY >" : "CONFIRM";
    }
}
