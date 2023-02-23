using System;
using UnityEngine;

[Serializable]
public class CardAction 
{
    public int id;
    public int parameter;

    public CardAction(int _id,int _param)
    {
        this.id = _id;
        this.parameter = _param;
    }
}
