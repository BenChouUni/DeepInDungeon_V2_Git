using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

//[CreateAssetMenu(fileName ="BleedEffect",menuName ="Asset/CreateEffect/Bleed")]
public class SolidEffect : StateEffect
{
    public SolidEffect(Character _myCharacter) : base("堅硬", false, StateEffectType.Solid, LayerConsumeType.TurnEnd, _myCharacter)
    {
    }

    public override void AtTurnEnd()
    {
        myCharacter.AddShield(Layer);
        //造成純粹傷害
        Debug.Log("判定Solid");
        ConsumeLayer();
    }

    public override string EffectDescription()
    {
        return "堅硬：每回合結束獲得"+ Layer + "點防禦";
    }
}
