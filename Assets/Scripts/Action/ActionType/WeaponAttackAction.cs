using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackAction : CardActionBase
{
    public override ActionType type => ActionType.WeaponAttack;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("Use {0} deal {1} damage",parameter.WeaponData.weaponName ,parameter.value);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        
        //如果沒有目標就直接跳出
        if (parameter.Target == null) return;
        Debug.Log(ActionDescribe(parameter));
        Character targetCharater = parameter.Target;

        List<StateEffect> targetStateList = targetCharater.StateList;
        //這邊要計算公式
        float damagef = (parameter.value + parameter.WeaponData.atk);
        foreach (StateEffect item in targetStateList)
        {
            damagef *= item.AtReceiveDamage();
        }
        int damage = (int)damagef;
        
        targetCharater.GetDamage(damage);
        
    }
}
