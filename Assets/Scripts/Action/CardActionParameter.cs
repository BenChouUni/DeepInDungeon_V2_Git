using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 傳入CardAction使用的參數組合
/// </summary>
[System.Serializable]
public class CardActionParameter 
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
                target= BattleMainManager.instance.GetCharacterByType(targetType);
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
                self = BattleMainManager.instance.GetCharacterByType(selfType);
            }
            return self;
        }
    }
    private WeaponData weaponData = null;
    [HideInInspector]
    public WeaponData WeaponData
    {
        get
        {
            if (weaponData == null)
            {
                Debug.LogError("Actionparameter WeapnData has no value");
            }
            return weaponData;
        }
    }
    
    private StateEffect myEffect = null;
    public StateEffect StateEffect
    {
        get
        {
            if (myEffect==null)
            {
                myEffect = EffectFactory.GetStatusEffect(stateEffectType);
            }
            return myEffect;
        }
    }



    public void SetWeponaData(WeaponData _weaponData)
    {
        this.weaponData = _weaponData;
    }
}
