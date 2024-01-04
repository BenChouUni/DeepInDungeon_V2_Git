using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferEffect : StateEffect
{
    public BufferEffect(Character _myCharacter) : base("緩衝", false, StateEffectType.Buffer, LayerConsumeType.AfterBeUse,_myCharacter)
    {

    }

    /// <summary>
    /// withstand one time damage
    /// </summary>
    /// <returns></returns>
    public override float AtReceiveDamage()
    {
        Debug.Log("Use Buffer !! ");
        if(Layer > 0)
        {
            return 0f;
        }
        else
        {
            return 1f;
        }
    }

    public override string EffectDescription()
    {
        return "緩衝：";
    }
    public override void AfterBeUse(ActionType type)
    {
        if(type == ActionType.WeaponAttack || type == ActionType.UnarmedDamage || type == ActionType.PureDamage || type == ActionType.ShieldDamage)
        {
            Debug.Log(type + "!!!");
            if(Layer > 0)
            {
                ConsumeLayer();
            }
        }
    }
}
