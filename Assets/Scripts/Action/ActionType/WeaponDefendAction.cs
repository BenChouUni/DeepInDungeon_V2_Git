using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDefendAction : CardActionBase
{
    public override ActionType type => ActionType.WeaponDefend;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return "Use Wepon to get shield";
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.target == null) return;
        Character targetCharater = parameter.target;
        //這邊要計算公式
        int shield = parameter.value + parameter.weaponData.def;


        targetCharater.AddShield(shield);
    }
}
