using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackAction : CardActionBase
{
    public override ActionType type => ActionType.WeaponAttack;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("使用{0}，造成{1}點傷害",parameter.WeaponData.weaponName , ValueCalculator.DmgCalculate(parameter.Self, parameter.TargetList[0], parameter.value + parameter.WeaponData.atk));
        
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.TargetList == null) return;
        Debug.Log(ActionDescribe(parameter));
        foreach (Character targetCharater in parameter.TargetList)
        {
            

            List<StateEffect> targetStateList = targetCharater.StateList;

            Character selCharacter = parameter.Self;
            List<StateEffect> myStateList = selCharacter.StateList;

            //這邊要計算公式
            float damagef = (parameter.value);
            int damage = ValueCalculator.DmgCalculate(parameter.Self, targetCharater, damagef + parameter.WeaponData.atk);
            //reduce layer after weaponaction

            foreach (StateEffect item in myStateList)
            {
                item.AfterUse(type);
            }

            foreach (StateEffect item in targetStateList)
            {
                item.AfterBeUse(type);
            }

            Debug.Log(damage);
            targetCharater.GetDamage(damage);
        }
        

        

        
    }
}
