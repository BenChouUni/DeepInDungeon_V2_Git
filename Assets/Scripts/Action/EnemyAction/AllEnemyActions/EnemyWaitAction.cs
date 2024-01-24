using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyWaitAction : EnemyActionBase
{
    public override ActionType type => ActionType.Wait;

    public override void DoEnemyAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        Debug.Log("Enemy Wait...");
    }

}
