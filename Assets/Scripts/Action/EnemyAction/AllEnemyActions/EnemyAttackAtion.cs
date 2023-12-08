using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyAttackAtion : EnemyActionBase
{
    public override ActionType type => ActionType.WeaponAttack;

    public override void DoAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        Debug.Log("敵人使用基本攻擊");

        if (parameter.TargetList == null) {
            Debug.LogError("no target");
            return;
        }

        //Character item = parameter.Target;
        //int dmg = ValueCalculator.DmgCalculate(parameter.Self, item, parameter.value + enemyData.ATK);
        //item.GetDamage(dmg);
        foreach (Character item in parameter.TargetList)
        {
            int dmg = ValueCalculator.DmgCalculate(parameter.Self, item, parameter.value + enemyData.ATK);
            item.GetDamage(dmg);
        }
    }
}
