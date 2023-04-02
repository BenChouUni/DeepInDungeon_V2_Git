using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理狀態
/// </summary>
public class StatusEffectManager : MonoSingleton<StatusEffectManager>
{
    public EffectList playrEffectList;
    public EffectList enemyEffectList;

    //從場景拖入
    public GameObject effectPerfab;
    public Transform playerEffectPanel;
    public Transform enemyEffectPanel;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isPlayer">true is player and false is enemy</param>
    /// <param name="effect"></param>
    public void AddEffect(bool isPlayer,StatusEffect effect)
    {
        
    }
    public void ShowEffect() {
        

    }


    
}
