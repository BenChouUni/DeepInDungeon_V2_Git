using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyActionFactory
{
    /// <summary>
    /// 根據傳入類型生成對應EnemyActionBase 子類
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static EnemyActionBase GetEnemyAction(EnemyActionType type)
    {
        EnemyActionBase enemyActionBase = null;
        switch (type)
        {
            case EnemyActionType.Attack:
                enemyActionBase = new EnemyAttackAtion();
                break;
            case EnemyActionType.Defend:
                enemyActionBase = new EnemyDefendActions();
                break;
        }

        return enemyActionBase;
    }
}
