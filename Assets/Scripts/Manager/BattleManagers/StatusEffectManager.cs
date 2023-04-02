using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理敵人跟玩家身上的狀態列
/// </summary>
public class StatusEffectManager : MonoSingleton<StatusEffectManager>
{
    public EffectList playerEffectList;
    public EffectList enemyEffectList;

    //public GameObject effectPerfab;
    //public Transform playerEffectPanel;
    //public Transform enemyEffectPanel;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isPlayer">true is player and false is enemy</param>
    /// <param name="effect"></param>
    public void AddEffect(TargetType target,StatusEffect effect,int layer)
    {
        if (target == TargetType.Player)
        {
            playerEffectList.AddStatusEffect(effect,layer);
        }
        else
        {
            enemyEffectList.AddStatusEffect(effect, layer);
        }
    }
    public void ShowEffect() {
        playerEffectList.ShowAllIcons();
        enemyEffectList.ShowAllIcons();

    }


    
}
