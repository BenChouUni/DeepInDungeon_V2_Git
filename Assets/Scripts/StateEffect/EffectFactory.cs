using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 給予type跟parameter來創建
/// </summary>
public static class EffectFactory 
{
    public static StateEffect GetStatusEffect(StateEffectType effectType)
    {
        switch (effectType)
        {
            case StateEffectType.Wound:
                return new WoundEffect();
                
            case StateEffectType.Bleed:
                return new BleedEffect();

            case StateEffectType.Weak:
                return new WeakEffect();
            default:
                return null;
                
        }
        
    }
}


