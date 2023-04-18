using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="BleedEffect",menuName ="Asset/CreateEffect/Bleed")]
public class BleedEffect : StateEffect
{
    public BleedEffect():base("Bleed",false,StateEffectType.Bleed,LayerConsumeType.AfterUse) {
    }

    public override void AtTurnEnd()
    {
        //造成純粹傷害
        Debug.Log("判定Bleed");
        ConsumeLayer();
        //CardAction cardAction = new CardAction(ActionType.PureDamage,)
        //ActionManager.instance.DoAction();
    }
}
