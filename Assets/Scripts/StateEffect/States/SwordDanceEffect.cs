using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDanceEffect : StateEffect
{
    // Start is called before the first frame update
    public SwordDanceEffect(Character _myCharacter) : base("劍舞", false, StateEffectType.SwordDance, LayerConsumeType.TurnEnd, _myCharacter)
    {

    }

    public override void AfterUse(ActionType type)
    {
        if (type == ActionType.WeaponAttack || type == ActionType.UnarmedDamage || type == ActionType.ShieldDamage)
        {
            while (Layer != 0)
            {
                ConsumeLayer();
            }
        }
    }

    public override string EffectDescription()
    {
        return "聚精會神：每次造成傷害時消耗所有層數造成等同層數的額外傷害";
    }
}
