using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAtion : EnemyActionBase
{
    public override EnemyActionType type => EnemyActionType.Attack;

    public override void DoAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        
    }
}
