using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionParameter
{
    [Header("數值")]
    public int value;
    [Header("接受對象")]
    public TargetType targetType;
    //[Header("施放者")]
    //public CharacterType selfType;
    [Header("狀態類型，不一定會使用")]
    public StateEffectType stateEffectType;

    //可以被繼承override
    public virtual Character Opponent
    {
        get
        {
            if (target == null)
            {
                Debug.LogWarning("no target"); //沒有target不一定影響
                //target = BattleMainManager.instance.GetCharacterByType(targetType);
            }
            return target;
        }
    }
    protected Character target = null;
    
    public List<Character> TargetList
    {
        get
        {
            
            List<Character> list = new List<Character>();
            switch (targetType)
            {
                case TargetType.Self:
                    list.Add(self);
                    break;
                case TargetType.Opponent:
                    list.Add(Opponent);
                    break;
                case TargetType.AllEnemies:
                    foreach (EnemyData item in EnemyManager.instance.enemyGroupData.enemies)
                    {
                        list.Add(item);
                    }
                    break;
                case TargetType.Random:
                    Debug.LogWarning("Random還沒實作");
                    break;
                default:
                    break;
            }
            return list;
        }
    }
    protected Character self = null;
    public Character Self
    {
        get
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }

            return self;
        }
    }

    //private StateEffect myEffect = null;
    //public StateEffect StateEffect
    //{
    //    get
    //    {
    //        if (myEffect == null)
    //        {
    //            myEffect = EffectFactory.GetStatusEffect(stateEffectType, Target);
    //        }
    //        return myEffect;
    //    }
    //}

    /// <summary>
    /// 只傳入一個target，只有玩家使用卡牌要指定目標時使用
    /// </summary>
    /// <param name="_target"></param>
    public virtual void setTarget(Character _target)
    {
        this.target = _target;
    }

    public void setSelf(Character _self)
    {
        this.self = _self;
    }
}
