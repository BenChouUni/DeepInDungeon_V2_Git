using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnarmedDamageAction : CardActionBase
{
    public override ActionType type => ActionType.UnarmedDamage;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("Attack {0}, {1}Damage", parameter.targetType, ValueCalculator.DmgCalculate(parameter.Self, parameter.TargetList[0], parameter.value));
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //�p�G�S���ؼдN�������X
        if (parameter.TargetList == null) return;
        foreach (Character targetCharater in parameter.TargetList)
        {
            List<StateEffect> targetStateList = targetCharater.StateList;

            Character selCharacter = parameter.Self;
            List<StateEffect> myStateList = selCharacter.StateList;


            //�o��n�p�⤽��
            float damagef = (parameter.value);
            int damage = ValueCalculator.DmgCalculate(parameter.Self, targetCharater, damagef);
            foreach (StateEffect item in myStateList)
            {
                item.AfterUse(type);
            }
            Debug.Log(damage);
            targetCharater.GetDamage(damage);
        }


    }
}
