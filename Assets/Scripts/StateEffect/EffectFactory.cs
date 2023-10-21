using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 給予type跟parameter來創建
/// </summary>
public static class EffectFactory 
{
    public static StateEffect GetStatusEffect(StateEffectType effectType, Character _myCharacter)
    {
        switch (effectType)
        {
            case StateEffectType.Wound:
                return new WoundEffect(_myCharacter);
                
            case StateEffectType.Bleed:
                return new BleedEffect(_myCharacter);

            case StateEffectType.Weak:
                return new WeakEffect(_myCharacter);
            case StateEffectType.Power:
                return new PowerEffect(_myCharacter);
            case StateEffectType.Buffer:
                return new BufferEffect(_myCharacter);
            case StateEffectType.Concentrate:
                return new ConcentrateEffect(_myCharacter);
            default:
                return null;
                
        }
        
    }
}


