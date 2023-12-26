using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGiveEffect : EnemyActionBase
{
    public override ActionType type => ActionType.Give;

    public override void DoEnemyAction(EnemyActionParameter parameter, EnemyData enemyData)
    {
        Debug.LogFormat("敵人施加{0}{1}層", parameter.stateEffectType,parameter.value);

        foreach (Character item in parameter.TargetList)
        {

            StateEffect stateEffect = EffectFactory.GetStatusEffect(parameter.stateEffectType, item);
            stateEffect.setLayer(parameter.value);
            item.AddStateEffect(stateEffect);
        }

        //Character item  = parameter.Target;
        

        //    StateEffect stateEffect = EffectFactory.GetStatusEffect(parameter.stateEffectType, item);
        //    stateEffect.setLayer(parameter.value);
        //    item.AddStateEffect(stateEffect);
        

    }
}
