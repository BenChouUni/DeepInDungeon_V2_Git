using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 給予type跟parameter來創建
/// </summary>
public static class EffectFactory 
{
    public static StatusEffect GetStatusEffect(EffectEnum effectType)
    {
        switch (effectType)
        {
            case EffectEnum.Wound:
                return new WoundEffect();
                
            case EffectEnum.Bleed:
                return new BleedEffect();
                
            default:
                return null;
                
        }
        
    }
}


