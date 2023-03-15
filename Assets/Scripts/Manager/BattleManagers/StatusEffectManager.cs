using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理狀態
/// </summary>
public class StatusEffectManager : MonoSingleton<StatusEffectManager>
{
    public List<StatusEffect> playerEffects = new List<StatusEffect>();

    public List<StatusEffect> enemyEffects = new List<StatusEffect>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isPlayer">true is player and false is enemy</param>
    /// <param name="effect"></param>
    public void AddEffect(bool isPlayer,StatusEffect effect)
    {
        if (isPlayer)
        {
            playerEffects.Add(effect);
        }
        else
        {
            enemyEffects.Add(effect);
        }
    }
    public void ShowEffect() { }


    
}
