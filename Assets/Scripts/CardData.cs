using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public int id;
    public string cardName;
    public int cost;

    public CardData()
    {
        this.id = 0;
        this.cardName = "";
        this.cost = 0;
    }
    public CardData(string _cardName) : this()
    {
        this.cardName = _cardName;
    }


}


