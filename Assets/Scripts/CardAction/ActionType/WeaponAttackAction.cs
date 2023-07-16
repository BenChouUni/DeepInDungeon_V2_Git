using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackAction : CardActionBase
{
    public override ActionType type => ActionType.WeaponAttack;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("使用{0}，造成{1}點傷害",parameter.WeaponData.weaponName ,parameter.value + parameter.WeaponData.atk);
        
    }

    public override void DoAction(CardActionParameter parameter)
    {
        
        //如果沒有目標就直接跳出
        if (parameter.Target == null) return;
        Debug.Log(ActionDescribe(parameter));
        Character targetCharater = parameter.Target;

        List<StateEffect> targetStateList = targetCharater.StateList;

        Character selCharacter = parameter.Self;
        List<StateEffect> myStateList = selCharacter.StateList;

        //這邊要計算公式
        float damagef = (parameter.value + parameter.WeaponData.atk);

        foreach (StateEffect item in myStateList)
        {
            damagef += item.AddExtraDamage();
        }
        foreach (StateEffect item in myStateList)
        {
            damagef *= item.AtDealDamage();
        }
        foreach (StateEffect item in targetStateList)
        {
            damagef *= item.AtReceiveDamage();
        }
        int damage = (int)damagef;
        
        targetCharater.GetDamage(damage);
        
    }
}
