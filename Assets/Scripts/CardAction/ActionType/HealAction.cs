using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAction : CardActionBase
{
    public override ActionType type => ActionType.Heal;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("restore {0} health", parameter.value);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        Character self = parameter.Self;

        self.HpState.RestoreHp(parameter.value);
    }
}
