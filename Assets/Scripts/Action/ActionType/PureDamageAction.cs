using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 不透過武器的純粹傷害
/// </summary>
public class PureDamageAction : CardActionBase
{
    public override ActionType type => ActionType.PureDamage;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return "Deal Damage";
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.Target == null) return;
        Character targetCharater = parameter.Target;

        List<StateEffect> targetStateList = targetCharater.StateList;
        //這邊要計算公式
        float damagef = (parameter.value);
        foreach (StateEffect item in targetStateList)
        {
            damagef *= item.AtReceiveDamage();
        }
        int damage = (int)damagef;

        targetCharater.GetDamage(damage);
    }
}
