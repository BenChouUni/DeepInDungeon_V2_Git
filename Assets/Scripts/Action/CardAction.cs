using System;
using UnityEngine;


public enum ActionType
{
    Attack,
    Defend,
    DrawCard,
}
[Serializable]
public class CardAction 
{
    public ActionType type;
    public int parameter;

    public CardAction(ActionType _type, int _param)
    {
        this.type = _type;
        this.parameter = _param;
    }
}
