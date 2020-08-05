using UnityEngine;
using UnityEngine.UI;

public class PodiumOverlayUI : MonoBehaviour
{
    [SerializeField] 
    private Image sprite;
    [SerializeField] 
    private Text rankReady;
    [SerializeField] 
    private Text killsNum;
    [SerializeField] 
    private Text deathsNum;
    private string rank_;
    
    public void UpdatePlayer(PlayerStats data)
    {
        CharacterData character = data.CharacterData;
        sprite.sprite = character.Sprite;
        killsNum.text = data.PlayerKills.ToString();
        deathsNum.text = data.PlayerDeaths.ToString();
    }

    public void UpdateRank(string rank)
    {
        rankReady.text = rank;
        rank_ = rank;
    }

    public void UpdateReady(bool isReady)
    {
        rankReady.text = (isReady) ? "READY >" : rank_;
    }
}
