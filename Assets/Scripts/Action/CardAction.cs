using System;
using UnityEngine;

public enum TargetType
{
    Player,Enemy
}
public enum ActionType
{
    DealDamage,
    Defend,
    DrawCard,
    Heal,
    Give,
}
[Serializable]
public class CardAction 
{
    public ActionType type;
    public int parameter;
    public TargetType target;

    public StatusType statusType;

    /// <summary>
    /// 沒有賦予狀態
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_param"></param>
    /// <param name="_target"></param>
    public CardAction(ActionType _type, int _param,TargetType _target)
    {
        this.type = _type;
        this.parameter = _param;
        this.target = _target;
        statusType = StatusType.None;
    }

    public CardAction(ActionType _type, int _param, TargetType _target,StatusType _statusType)
    {
        this.type = _type;
        this.parameter = _param;
        this.target = _target;
        statusType = _statusType;
    }
}
