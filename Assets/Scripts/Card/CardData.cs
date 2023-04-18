using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public int id;
    public string cardName;
    public int cost;
    public int initialnum;

    public WeaponData WeaponData
    {
        get { return weaponData; }
    }
    [SerializeField]
    private WeaponData weaponData;
    //效果
    public List<CardActionSet> cardActions;

    public CardData()
    {
        this.id = 0;
        this.cardName = "";
        this.cost = 0;
        initialnum = 0;
        cardActions = new List<CardActionSet>();
        weaponData = null;
    }
    public CardData(int _id, string _cardName, int _cost, int _initialnum) : this()
    {
        this.id = _id;
        this.cardName = _cardName;
        this.cost = _cost;
        initialnum = _initialnum;
    }

    public void SetWeaponData(WeaponData data)
    {
        this.weaponData = data;
    }

}


