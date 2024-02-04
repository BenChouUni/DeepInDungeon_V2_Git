using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

//[CreateAssetMenu(fileName ="BleedEffect",menuName ="Asset/CreateEffect/Bleed")]
public class SolidEffect : StateEffect
{
    public SolidEffect(Character _myCharacter) : base("��w", false, StateEffectType.Solid, LayerConsumeType.Never, _myCharacter)
    {
    }

    public override void AtTurnEnd()
    {
        myCharacter.AddShield(Layer);
        //�y���º�ˮ`
        Debug.Log("�P�wSolid");
    }

    public override string EffectDescription()
    {
        return "��w�G�C�^�X������o"+ Layer + "�I���m";
    }
}
