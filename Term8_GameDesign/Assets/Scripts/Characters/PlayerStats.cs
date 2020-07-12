using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private int playerNum;      // starts from 0!
    [SerializeField]
    private CharacterData character;
    [SerializeField]
    private int Weets;
    [SerializeField]
    private int qtyPotion2;
    [SerializeField]
    private int qtyPotion3;
    
    [SerializeField]
    private int _playerKills = 0;
    [SerializeField]
    private int _playerDeaths = 0;
    [SerializeField]
    private int _playerHealth = 3;

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

    public void resetPlayerHealth()
    {
        _playerHealth = 3;
    }
}
