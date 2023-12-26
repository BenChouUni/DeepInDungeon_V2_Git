using System;
using UnityEngine;

[Serializable]
public abstract class EnemyActionBase
{
    public abstract ActionType type { get; }

    protected EnemyActionBase() { }

    public abstract void DoEnemyAction(EnemyActionParameter parameter,EnemyData enemyData);
}
