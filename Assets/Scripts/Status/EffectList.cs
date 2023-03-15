using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自我檢查的狀態列物件
/// </summary>
public class EffectList : MonoBehaviour
{
    private List<EffectLayerObject> statusEffectList;
    public List<EffectLayerObject> StatusEffectList
    {
        get { return statusEffectList; }
    }
    
    public EffectList() { statusEffectList = null; }

    public void AddStatusEffect(StatusEffect statusEffect,int layer)
    {
        EffectLayerObject ELObj = statusEffectList.Find(elobj => elobj.statusEffect == statusEffect);
        if (ELObj != null)
        {
            ELObj.layer += layer;
        }
        else
        {
            EffectLayerObject newObj = new EffectLayerObject(statusEffect, layer);
            statusEffectList.Add(newObj);
        }
    }

    
}
