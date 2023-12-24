using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferEffect : StateEffect
{
    public BufferEffect(Character _myCharacter) : base("緩衝", false, StateEffectType.Buffer, LayerConsumeType.AfterUse,_myCharacter)
    {

    }

    /// <summary>
    /// withstand one time damage
    /// </summary>
    /// <returns></returns>
    public override float AtReceiveDamage()
    {
        Debug.Log("�P�w��Buffer");
        ConsumeLayer();
        return 0f;
    }


    public override void AfterUse(ActionType type)
    {

    }
}
