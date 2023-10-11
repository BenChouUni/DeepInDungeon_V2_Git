using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class EnemyActionSet
{
    [Header("攻擊模式")]
    public EnemyActionType actionType;

    public EnemyActionBase EnemyActionBase
    {
        get
        {
            if (enemyActionBase==null )
            {
                enemyActionBase = EnemyActionFactory.GetEnemyAction(actionType);
            }
            return enemyActionBase;
        }
    }

    private EnemyActionBase enemyActionBase;

    public EnemyActionParameter enemyActionParameter;

    public void DoAction(EnemyData enemyData)
    {
        EnemyActionBase.DoAction(enemyActionParameter, enemyData);
    }
}
