using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPowerEffect : StateEffect
{
    public TempPowerEffect(Character _myCharacter) : base("�O�q", false, StateEffectType.Power, LayerConsumeType.TurnEnd, _myCharacter)
    {

    }

    public override int AddExtraDamage()
    {
        return Layer;
    }

    public override string EffectDescription()
    {
        return "�O�q�G�C�h�B�~�W�[�@�I�y�����ˮ`";
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
