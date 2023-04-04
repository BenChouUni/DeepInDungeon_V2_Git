using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BleedEffect",menuName ="Asset/CreateEffect/Bleed")]
public class BleedEffect : StatusEffect
{
    public BleedEffect():base("Bleed",false,EffectEnum.Bleed,LayerConsumeType.AfterUse) {
    }

    public override void AtTurnEnd()
    {
        //造成純粹傷害
        Debug.Log("判定Bleed");
        //CardAction cardAction = new CardAction(ActionType.PureDamage,)
        //ActionManager.instance.DoAction();
    }
}
