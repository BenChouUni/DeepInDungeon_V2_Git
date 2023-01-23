using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public int id;
    public string cardName;
    public int cost;

    //效果
    public List<Action> cardAction;

    public CardData()
    {
        this.id = 0;
        this.cardName = "";
        this.cost = 0;
        cardAction = null;
    }
    public CardData(int _id,string _cardName,int _cost) : this()
    {
        this.id = _id;
        this.cardName = _cardName;
        this.cost = _cost;
    }


}


