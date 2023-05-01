using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerData : Character,ICloneable
{
    [SerializeField]
    private WeaponData mainWeaponData;
    public WeaponData MainWeaponData
    {
        get { return mainWeaponData; }
        set { mainWeaponData = value; }
    }
    [SerializeField]
    private WeaponData supportWeaponData;
    public WeaponData SupportWeaponData
    {
        get { return supportWeaponData; }
        set { supportWeaponData = value; }
    }
    [SerializeField]
    private int coin;
    public int Coin
    {
        get { return coin; }
    }
    [SerializeField]
    private int energy;
    public int Energy
    {
        get { return energy; }
    }

    public Action<int> coinDisplay;
    public Action<int> energyDisplay;
    /// <summary>
    /// 預設初始化 "no name", 50, 0
    /// </summary>
    public PlayerData() : base("no name", 50, 0)
    {
        coin = 0;
        mainWeaponData = null;
        supportWeaponData = null;
        this.energy = 3;
        this.targetType = CharaterType.Player; //兩構造函數應該要整合
    }
    public PlayerData(string _name, int _maxHp, int _shield,int _energy) :base(_name,_maxHp,_shield)
    {
        coin = 0;
        mainWeaponData = null;
        supportWeaponData = null;
        this.energy = _energy;
        this.targetType = CharaterType.Player;
    }


    public void CoinAdd(int num)
    {
        if (num <= 0) return;
        coin += num;
        coinDisplay?.Invoke(coin);

    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
