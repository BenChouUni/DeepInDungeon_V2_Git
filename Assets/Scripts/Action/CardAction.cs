using System;
using UnityEngine;

public enum TargetType
{
    Player,Enemy
}
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
    public TargetType target;


    public CardAction(ActionType _type, int _param,TargetType _target)
    {
        this.type = _type;
        this.parameter = _param;
        this.target = _target;
    }
}
