using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefendActions : EnemyActionBase
{
    public override EnemyActionType type => EnemyActionType.Defend;

    public override void DoAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        Debug.Log("敵人使用防禦");
        foreach (Character item in parameter.TargetList)
        {
            int shield = ValueCalculator.DefCalculate(parameter.Self, item, parameter.value);
            item.AddShield(shield);
        }
    }
}
