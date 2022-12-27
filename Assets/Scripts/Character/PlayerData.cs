using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Character
{
    private WeaponData mainWeaponData;
    public WeaponData MainWeaponData
    {
        get { return mainWeaponData; }
    }
    private WeaponData supportWeaponData;
    public WeaponData SupportWeaponData
    {
        get { return supportWeaponData; }
    }

    public PlayerData(string _name, int _maxHp, int _shield) :base(_name,_maxHp,_shield)
    {
        
    }
}
