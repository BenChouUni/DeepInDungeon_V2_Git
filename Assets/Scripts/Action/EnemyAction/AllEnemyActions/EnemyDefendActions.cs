using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefendActions : EnemyActionBase
{
    public override ActionType type => ActionType.WeaponDefend;

    public override void DoAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        Debug.Log("敵人使用防禦");
        //Character item = parameter.Self;
        //int shield = ValueCalculator.DefCalculate(parameter.Self, item, parameter.value);
        //item.AddShield(shield);
        foreach (Character item in parameter.TargetList)
        {
            int shield = ValueCalculator.DefCalculate(parameter.Self, item, parameter.value);
            item.AddShield(shield);
        }
    }
}
