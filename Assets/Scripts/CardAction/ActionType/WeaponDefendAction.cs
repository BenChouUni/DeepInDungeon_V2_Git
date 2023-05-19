using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDefendAction : CardActionBase
{
    public override ActionType type => ActionType.WeaponDefend;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("Use {0} get {1} shield", parameter.WeaponData.weaponName, parameter.value + parameter.WeaponData.def);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.Target == null) return;
        Debug.Log(ActionDescribe(parameter));
        Character targetCharater = parameter.Target;
        //這邊要計算公式
        int shield = parameter.value + parameter.WeaponData.def;
        

        targetCharater.AddShield(shield);
    }
}
