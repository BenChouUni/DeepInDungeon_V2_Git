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
    /// when attack, add layer, and then consume all layer
    /// </summary>
    /// <returns></returns>
    public override float AtDealDamage()
    {
        int num = Layer;
        
        return num;
        
    }

    public override void AfterUse(ActionType type)
    {
        if(type == ActionType.WeaponAttack || type == ActionType.RealDefend)
        {
            while (Layer != 0)
            {
                ConsumeLayer();
            }
        }
    }

}
