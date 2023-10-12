using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackAction : CardActionBase
{
    public override ActionType type => ActionType.WeaponAttack;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("使用{0}，造成{1}點傷害",parameter.WeaponData.weaponName , parameter.value + parameter.WeaponData.atk);
        
    }

    public override void DoAction(CardActionParameter parameter)
    {
        
        //如果沒有目標就直接跳出
        if (parameter.TargetList == null) return;
        Debug.Log(ActionDescribe(parameter));
        foreach (Character item in parameter.TargetList)
        {
            int dmg = ValueCalculator.DmgCalculate(parameter.Self, item, parameter.value + parameter.WeaponData.atk);
            item.GetDamage(dmg);
        }


        
    }


}
