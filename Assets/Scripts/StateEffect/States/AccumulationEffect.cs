using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AccumulationEffect : StateEffect
{
    public AccumulationEffect(Character _myCharacter) : base("�W�O", false, StateEffectType.Accumulation, LayerConsumeType.TurnEnd, _myCharacter)
    {

    }

    public override float AtDealDamage()
    {
        return 1f + 0.25F * Layer;
    }

    public override void AfterUse(ActionType type)
    {
        if (type == ActionType.WeaponAttack || type == ActionType.UnarmedDamage)
        {
            if (Layer >= 3)
            {
                overflow();
            }
        }
    }

    public override void AtTurnEnd()
    {
        consumeall();
    }

    public void overflow()
    {
        consumeall();
    }


    public void consumeall()
    {
        while (Layer > 0)
        {
            ConsumeLayer();
        }
    }

    public override string EffectDescription()
    {
        return "蓄力：每一層蓄力增加1/4傷害，最多累積三層";
    }
}
