using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerData : Character
{
    [SerializeField]
    private WeaponData mainWeaponData;
    public WeaponData MainWeaponData
    {
        get;set;
    }
    [SerializeField]
    private WeaponData supportWeaponData;
    public WeaponData SupportWeaponData
    {
        get;set;
    }
    [SerializeField]
    private int coin;
    public int Coin
    {
        get { return coin; }
    }
    /// <summary>
    /// 預設初始化 "no name", 50, 0
    /// </summary>
    public PlayerData() : base("no name", 50, 0)
    {
        coin = 0;
    }
    public PlayerData(string _name, int _maxHp, int _shield) :base(_name,_maxHp,_shield)
    {
        coin = 0;
        mainWeaponData = null;
        supportWeaponData = null;
    }

    public void CoinAdd(int num)
    {
        coin += 0;
    }
}
