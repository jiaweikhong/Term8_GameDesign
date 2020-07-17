using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private int playerNum;      // starts from 0!
    [SerializeField]
    private CharacterData _character;
    [SerializeField]
    private int _weets = 200;
    [SerializeField]
    private int _secondaryPotionQty = 0;
    [SerializeField]
    private SpecialPotionData _specialPotion;
    [SerializeField]
    private int _specialPotionQty = 0;

    public CharacterData CharacterData
    {
        get
        { return _character; }
        set
        { _character = value; }
    }
    public int Weets
    {
        get
        { return _weets; }
        set
        { _weets = value; }
    }
    public int SecondaryPotionQty
    {
        get
        { return _secondaryPotionQty; }
        set
        { _secondaryPotionQty = value; }
    }
    public SpecialPotionData specialPotion
    {
        get
        { return _specialPotion; }
        set
        { _specialPotion = value; }
    }
    public int SpecialPotionQty
    {
        get
        { return _specialPotionQty; }
        set
        { _specialPotionQty = value; }
    }

    [SerializeField]
    private int _playerKills = 0;
    [SerializeField]
    private int _playerDeaths = 0;
    [SerializeField]
    private int _playerHealth = 3;
    [SerializeField]
    private int _qtyPotion2 = 0;
    [SerializeField]
    private int _qtyPotion3 = 0;

    public int PlayerKills
    {
        get
        { return _playerKills; }
        set
        { _playerKills = value; }
    }
    public int PlayerDeaths
    {
        get
        { return _playerDeaths; }
        set
        { _playerDeaths = value; }
    }
    public int PlayerHealth
    {
        get
        { return _playerHealth; }
        set
        { _playerHealth = value; }
    }
    public void ResetPlayerHealth()
    {
        _playerHealth = 3;
    }
    public void ResetGame()
    {
        _weets = 200;
        _secondaryPotionQty = 0;
        _specialPotionQty = 0;
        _playerKills = 0;
        _playerDeaths = 0;
        _playerHealth = 3;
    }

    public void UpdateBrew(int weets, int secQty, int speQty, SpecialPotionData special)
    {
        _weets = weets;
        _secondaryPotionQty = secQty;
        _specialPotionQty = speQty;
        _specialPotion = special;
    }
}
