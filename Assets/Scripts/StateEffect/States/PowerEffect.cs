using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEffect : StateEffect
{
    public  PowerEffect(Character _myCharacter) : base("力量", false, StateEffectType.Power, LayerConsumeType.Never,_myCharacter)
    {

    }

    /// <summary>
    /// �W�[�y�����ˮ`
    /// </summary>
    /// <returns></returns>
    public override int AddExtraDamage()
    {
        //Debug.Log("�P�w��Power" + Layer + "�h");
        return Layer;
    }

    public override string EffectDescription()
    {
        return "力量：每層額外增加一點造成的傷害";
    }
}
