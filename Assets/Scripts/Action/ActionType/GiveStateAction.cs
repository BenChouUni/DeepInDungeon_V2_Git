using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveStateAction : CardActionBase
{
    public override ActionType type => ActionType.Give;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("give {0}", parameter.StateEffect);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        Character targetCharater = parameter.Target;
        StateEffect stateEffect = parameter.StateEffect;
        stateEffect.setLayer(parameter.value);

        Debug.LogFormat("給予{0}狀態{1}層", stateEffect.effectType, parameter.value);
        targetCharater.AddStateEffect(stateEffect);

    }
}
