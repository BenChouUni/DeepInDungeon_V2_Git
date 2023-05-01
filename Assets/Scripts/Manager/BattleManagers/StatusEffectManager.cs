using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 改到player跟enemy自己manager上
/// </summary>
public class StatusEffectManager : MonoSingleton<StatusEffectManager>
{
    public EffectListDisplay playerEffectList;
    public EffectListDisplay enemyEffectList;

    //public GameObject effectPerfab;
    //public Transform playerEffectPanel;
    //public Transform enemyEffectPanel;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isPlayer">true is player and false is enemy</param>
    /// <param name="effect"></param>
    //public void AddEffect(CharaterType target,StateEffect effect,int layer)
    //{
    //    if (target == CharaterType.Player)
    //    {
    //        playerEffectList.AddStatusEffect(effect,layer);
    //    }
    //    else
    //    {
    //        enemyEffectList.AddStatusEffect(effect, layer);
    //    }
    //}
    //public void ShowEffect() {
    //    playerEffectList.ShowAllIcons();
    //    enemyEffectList.ShowAllIcons();

    //}

    //public List<StateEffect> GetTargetEffectList(CharaterType target) {
    //    if (target == CharaterType.Player)
    //    {
    //        return playerEffectList.StatusEffectList;
    //    }
    //    else
    //    {
    //        return enemyEffectList.StatusEffectList;
    //    }
    //}

    
}
