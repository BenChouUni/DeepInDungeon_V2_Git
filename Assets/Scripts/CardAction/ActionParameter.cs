using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionParameter
{
    [Header("數值")]
    public int value;
    [Header("接受對象")]
    public CharaterType targetType;
    [Header("施放者")]
    public CharaterType selfType;
    [Header("狀態類型，不一定會使用")]
    public StateEffectType stateEffectType;

    //由工廠產生
    private Character target = null;
    public Character Target
    {
        get
        {
            if (target == null)
            {
                //Debug.LogError("no target");
                //target = BattleMainManager.instance.GetCharacterByType(targetType);
            }
            return target;
        }
    }
    private Character self = null;
    public Character Self
    {
        get
        {
            if (self == null)
            {
                Debug.LogError("no self");
                //self = BattleMainManager.instance.GetCharacterByType(selfType);
            }
            return self;
        }
    }

    private StateEffect myEffect = null;
    public StateEffect StateEffect
    {
        get
        {
            if (myEffect == null)
            {
                myEffect = EffectFactory.GetStatusEffect(stateEffectType, Target);
            }
            return myEffect;
        }
    }

    public void setTarget(Character _target)
    {
        this.target = _target;
    }

    public void setSelf(Character _self)
    {
        this.self = _self;
    }
}
