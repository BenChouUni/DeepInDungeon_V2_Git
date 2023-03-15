using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 由於layer是動態的，不應該整合到StatusEffect中
/// </summary>
public class EffectLayerObject
{
    public StatusEffect statusEffect;
    public int layer;

    public EffectLayerObject(StatusEffect _statusEffect, int _layer)
    {
        this.statusEffect = _statusEffect;
        this.layer = _layer;
    }
}
