using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnarmedDamageAction : CardActionBase
{
    public override ActionType type => ActionType.UnarmedDamage;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("直接向{0}造成{1}點徒手傷害", parameter.targetType, parameter.value);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.Target == null) return;
        Character targetCharater = parameter.Target;

        List<StateEffect> targetStateList = targetCharater.StateList;

        Character selCharacter = parameter.Self;
        List<StateEffect> myStateList = selCharacter.StateList;


        //這邊要計算公式
        float damagef = (parameter.value);
        int damage = ValueCalculator.DmgCalculate(parameter, 0);
        foreach (StateEffect item in myStateList)
        {
            item.AfterUse(type);
        }
        Debug.Log(damage);
        targetCharater.GetDamage(damage);
    }
}
