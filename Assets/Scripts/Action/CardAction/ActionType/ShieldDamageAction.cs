using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
/// <summary>
/// �ϥΨ��m�O�i�����
/// </summary>
public class ShieldDamageAction : CardActionBase
{
    public override ActionType type => ActionType.ShieldDamage;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("�V{0}�y��{1}�I���m�ˮ`", parameter.targetType, parameter.Self.Shield);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //�p�G�S���ؼдN�������X
        if (parameter.TargetList == null) return;
        foreach (Character target in parameter.TargetList)
        {
            Character targetCharater = target;
            List<StateEffect> targetStateList = targetCharater.StateList;

            Character selCharacter = parameter.Self;
            List<StateEffect> myStateList = selCharacter.StateList;


            //�o��n�p�⤽��
            float damagef = (parameter.Self.Shield);

            int damage = (int)damagef;
            //Debug.Log(damage);

            targetCharater.GetDamage(damage);
        }

    }
}
