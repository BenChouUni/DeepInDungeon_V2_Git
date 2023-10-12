using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDefendAction : CardActionBase
{
    public override ActionType type => ActionType.WeaponDefend;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("使用{0}，獲得{1}點護甲", parameter.WeaponData.weaponName, parameter.value + parameter.WeaponData.def);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.TargetList == null) return;
        //Debug.Log(ActionDescribe(parameter));

        foreach (Character item in parameter.TargetList)
        {
            int shield = ValueCalculator.DefCalculate(parameter.Self, item,parameter.value + parameter.WeaponData.def);
            item.AddShield(shield);
        }
        

    }
}
