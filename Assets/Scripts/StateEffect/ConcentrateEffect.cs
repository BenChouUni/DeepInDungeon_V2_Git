using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentrateEffect : StateEffect
{
    // Start is called before the first frame update
    public ConcentrateEffect(Character _myCharacter) : base("聚精會神", false, StateEffectType.Concentrate, LayerConsumeType.AfterUse, _myCharacter)
    {

    }

    /// <summary>
    /// �y�����ˮ`���50%
    /// </summary>
    /// <returns></returns>
    public override float AtDealDamage()
    {
        //Debug.LogFormat("Weak：{0}", Layer);
        int num = Layer;
        /*
        while (Layer != 0)
        {
            ConsumeLayer();
        }
        */
        
        return num;
        
    }

}
