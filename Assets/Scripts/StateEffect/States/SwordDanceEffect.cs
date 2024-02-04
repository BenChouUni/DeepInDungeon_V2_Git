using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDanceEffect : StateEffect
{
    // Start is called before the first frame update
    public SwordDanceEffect(Character _myCharacter) : base("�C�R", false, StateEffectType.SwordDance, LayerConsumeType.TurnEnd, _myCharacter)
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
        return "�E��|���G�C���y���ˮ`�ɮ��өҦ��h�Ƴy�����P�h�ƪ��B�~�ˮ`";
    }
}
