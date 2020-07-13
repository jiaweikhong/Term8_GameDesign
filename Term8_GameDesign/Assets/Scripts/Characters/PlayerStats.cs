using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private int playerNum;      // starts from 0!
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
        {
            return _playerKills;
        }

        set
        {
            _playerKills = value;
        }
    }

    public int PlayerDeaths
    {
        get
        {
            return _playerDeaths;
        }

        set
        {
            _playerDeaths = value;
        }
    }

    public int PlayerHealth
    {
        get
        {
            return _playerHealth;
        }

        set
        {
            _playerHealth = value;
        }
    }

    public int QtyPotion2
    {
        get
        {
            return _qtyPotion2;
        }

        set
        {
            _qtyPotion2 = value;
        }
    }

    public int QtyPotion3
    {
        get
        {
            return _qtyPotion3;
        }

        set
        {
            _qtyPotion3 = value;
        }
    }

    public void resetPlayerHealth()
    {
        _playerHealth = 3;
    }
}
