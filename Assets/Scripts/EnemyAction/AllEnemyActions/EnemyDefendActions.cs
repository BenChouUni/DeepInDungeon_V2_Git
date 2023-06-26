using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefendActions : EnemyActionBase
{
    public override EnemyActionType type => EnemyActionType.Defend;

    public override void DoAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        int def = parameter.value;
        //這邊可能有算式
        parameter.Target.AddShield(def);
    }
}
