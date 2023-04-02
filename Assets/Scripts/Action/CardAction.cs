using System;
using UnityEngine;

public enum TargetType
{
    Player,Enemy
}

[Serializable]
public class CardAction 
{
    public ActionType type;
    public int parameter;
    public TargetType target;

    public EffectEnum StatusEffect;

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
        StatusEffect = EffectEnum.NULL;
    }

    public CardAction(ActionType _type, int _param, TargetType _target,EffectEnum _effect)
    {
        this.type = _type;
        this.parameter = _param;
        this.target = _target;
        StatusEffect = _effect;
    }
}
