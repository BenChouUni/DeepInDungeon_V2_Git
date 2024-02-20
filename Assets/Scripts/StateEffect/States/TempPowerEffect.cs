using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPowerEffect : StateEffect
{
    public TempPowerEffect(Character _myCharacter) : base("力量", false, StateEffectType.Power, LayerConsumeType.TurnEnd, _myCharacter)
    {

    }

    public override int AddExtraDamage()
    {
        return Layer;
    }

    public override string EffectDescription()
    {
        return "力量：每層額外增加一點造成的傷害";
    }

    public override void AtTurnEnd()
    {
        while(Layer > 0)
        {
            ConsumeLayer();
        }
    }

    public override void AfterUse(ActionType type)
    {
        if (type == ActionType.WeaponAttack || type == ActionType.UnarmedDamage || type == ActionType.PureDamage || type == ActionType.ShieldDamage)
        {
            AddLayer(1);
        }
    }
}
