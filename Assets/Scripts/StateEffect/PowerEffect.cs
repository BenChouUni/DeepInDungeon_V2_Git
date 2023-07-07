using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEffect : StateEffect
{
    public  PowerEffect() : base("Power", false, StateEffectType.Power, LayerConsumeType.Never)
    {

    }

    /// <summary>
    /// �W�[�y�����ˮ`
    /// </summary>
    /// <returns></returns>
    public override int AddExtraDamage()
    {
        Debug.Log("�P�w��Power" + Layer + "�h");
        return Layer;
    }
}
