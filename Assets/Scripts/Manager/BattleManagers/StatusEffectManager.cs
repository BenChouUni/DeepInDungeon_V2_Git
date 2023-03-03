using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理狀態，掛載再分別目標身上
/// </summary>
public class StatusEffectManager : MonoBehaviour
{
    private List<StatusEffect> effects = new List<StatusEffect>();
    public List<StatusEffect> Effects
    {
        get { return effects; }
    }

    void Start()
    {
        
    }

    public void AddStatus(StatusType type,int layer)
    {

        StatusEffect effect = new StatusEffect(type, layer, true);
        this.effects.Add(effect);
    }

}
