using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// 跟CardActionParameter高度重複，或許該用繼承
/// </summary>
[Serializable]
public class EnemyActionParameter : ActionParameter
{

    //[Header("數值")]
    //public int value;
    //[Header("接受對象")]
    //public CharaterType targetType;
    //[Header("施放者")]
    //public CharaterType selfType;
    //[Header("狀態類型，不一定會使用")]
    //public StateEffectType stateEffectType;

    public override Character Opponent => BattlePlayerDataManager.instance.battleplayerData;
    ////由工廠產生
    //private Character target = null;

    //private Character self = null;
    //public Character Self
    //{
    //    get
    //    {
    //        if (self == null)
    //        {
    //            self = BattleMainManager.instance.GetCharacterByType(selfType);
    //        }
    //        return self;
    //    }
    //}



    //private StateEffect myEffect = null;
    //public StateEffect StateEffect
    //{
    //    get
    //    {
    //        if (myEffect == null)
    //        {
    //            myEffect = EffectFactory.GetStatusEffect(stateEffectType,Target);
    //        }
    //        return myEffect;
    //    }
    //}


}
