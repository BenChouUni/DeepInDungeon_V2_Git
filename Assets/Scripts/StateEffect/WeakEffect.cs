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
    /// 造成的傷害減少50%
    /// </summary>
    /// <returns></returns>
    public override float AtDealDamage()
    {
        Debug.Log("判定到Weak");
        return 0.5f;
    }

    public override void AtTurnEnd()
    {
        ConsumeLayer();
    }
}
