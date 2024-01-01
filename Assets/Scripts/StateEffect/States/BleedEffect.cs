using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="BleedEffect",menuName ="Asset/CreateEffect/Bleed")]
public class BleedEffect : StateEffect
{
    public BleedEffect(Character _myCharacter) :base("流血",false,StateEffectType.Bleed,LayerConsumeType.AfterUse,_myCharacter) {
    }

    public override void AtTurnEnd()
    {
        //造成純粹傷害
        Debug.Log("判定Bleed");
        myCharacter.GetDamage(3, false);
        ConsumeLayer();
        //CardAction cardAction = new CardAction(ActionType.PureDamage,)
        //ActionManager.instance.DoAction();
    }

    public override string EffectDescription()
    {
        return "流血：每回合結束扣三點血，並減少一層";
    }
}
