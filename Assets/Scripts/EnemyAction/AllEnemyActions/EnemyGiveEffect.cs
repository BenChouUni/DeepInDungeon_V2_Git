using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGiveEffect : EnemyActionBase
{
    public override EnemyActionType type => EnemyActionType.GiveEffect;

    public override void DoAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        Debug.LogFormat("敵人施加{0}{1}層", parameter.stateEffectType,parameter.value);

        Character targetCharater = parameter.Target;
        StateEffect stateEffect = EffectFactory.GetStatusEffect(parameter.stateEffectType, targetCharater);
        stateEffect.setLayer(parameter.value);
        targetCharater.AddStateEffect(stateEffect);

    }
}
