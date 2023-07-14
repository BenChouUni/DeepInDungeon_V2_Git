using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferEffect : StateEffect
{
    public BufferEffect() : base("Buffer", false, StateEffectType.Buffer, LayerConsumeType.AfterUse)
    {

    }

    /// <summary>
    /// withstand one time damage
    /// </summary>
    /// <returns></returns>
    public override float AtReceiveDamage()
    {
        Debug.Log("§P©w¨ìBuffer");
        //ConsumeLayer();
        return 0f;
    }
}
