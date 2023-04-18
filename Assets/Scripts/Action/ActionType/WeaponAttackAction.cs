using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackAction : CardActionBase
{
    public override ActionType type => ActionType.WeaponAttack;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return "Use Weapon deal damage";
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.target == null) return;
        Character targetCharater = parameter.target;

        List<StateEffect> targetStateList = targetCharater.StateList;
        //這邊要計算公式
        float damagef = (parameter.value + parameter.weaponData.atk);
        foreach (StateEffect item in targetStateList)
        {
            damagef *= item.AtReceiveDamage();
        }
        int damage = (int)damagef;

        targetCharater.GetDamage(damage);
    }
}
