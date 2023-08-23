using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardData
{
    public int id;
    public string cardName;
    public int cost;
    public int initialnum;

    //卡片是否可以需要箭頭指向
    public bool isDirective;
    //public Sprite image;

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
        this.isDirective = false;
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
        foreach (CardActionSet item in cardActions)
        {
            item.actionParameter.SetWeponaData(weaponData);
        }
    }

}


