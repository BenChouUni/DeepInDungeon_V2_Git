using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 傳入CardAction使用的參數組合
/// </summary>
public class CardActionParameter : MonoBehaviour
{
    public readonly int value;
    public readonly Character target;
    public readonly Character self;
    public readonly WeaponData weaponData;
    public readonly CardData cardData;
    public readonly StateEffect effect;

    public CardActionParameter(int _value,Character _target,Character _self,CardData _cardData,StateEffect _effect)
    {
        this.value = _value;
        this.target = _target;
        this.self = _self;
        this.weaponData = _cardData.WeaponData;
        this.cardData = _cardData;
        this.effect = _effect;
    }
}
