using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEffect : StateEffect
{
    // Start is called before the first frame update
    public WeakEffect() : base("Weak", false, StateEffectType.Weak, LayerConsumeType.TurnEnd)
    {

    }

    /// <summary>
    /// �y�����ˮ`���50%
    /// </summary>
    /// <returns></returns>
    public override float AtDealDamage()
    {
        Debug.Log("�P�w��Weak");
        return 0.5f;
    }

    public override void AtTurnEnd()
    {
        ConsumeLayer();
    }
}
