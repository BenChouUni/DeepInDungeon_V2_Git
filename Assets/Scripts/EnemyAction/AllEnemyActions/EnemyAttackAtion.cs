using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAtion : EnemyActionBase
{
    public override EnemyActionType type => EnemyActionType.Attack;

    public override void DoAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        Debug.Log("敵人使用基本攻擊");

        if (parameter.Target == null) {
            Debug.LogError("no target");
            return;
        }
        
        Character targetCharater = parameter.Target;

        List<StateEffect> targetStateList = targetCharater.StateList;

        Character selCharacter = parameter.Self;
        List<StateEffect> myStateList = selCharacter.StateList;

        //這邊要計算公式
        //float damagef = parameter.value;
        //damagef += enemyData.ATK;

        //foreach (StateEffect item in myStateList)
        //{
        //    damagef += item.AddExtraDamage();
        //}

        //foreach (StateEffect item in targetStateList)
        //{
        //    damagef *= item.AtReceiveDamage();
        //}
        int damage = ValueCalculator.DmgCalculate(parameter, enemyData.ATK);
        parameter.Target.GetDamage(damage);
        //Debug.Log(parameter.Target.CharacterName + "受到攻擊");
    }
}
