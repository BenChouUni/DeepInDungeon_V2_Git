using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEffect : StateEffect
{
    public  PowerEffect() : base("Power", false, StateEffectType.Power, LayerConsumeType.Never)
    {

    }

    /// <summary>
    /// 增加造成的傷害
    /// </summary>
    /// <returns></returns>
    public override int AddExtraDamage()
    {
        Debug.Log("判定到Power" + Layer + "層");
        return Layer;
    }
}
