using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAtion : EnemyActionBase
{
    public override EnemyActionType type => EnemyActionType.Attack;

    public override void DoAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        Debug.Log("敵人使用基本攻擊");
        int dmg = parameter.value;
        dmg += enemyData.ATK;
        parameter.Target.GetDamage(dmg);
        Debug.Log(parameter.Target.CharacterName + "受到攻擊");
    }
}
