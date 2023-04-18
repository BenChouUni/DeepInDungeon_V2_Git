using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAction : CardActionBase
{
    public override ActionType type => ActionType.Heal;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return "restore heal";
    }

    public override void DoAction(CardActionParameter parameter)
    {
        Character self = parameter.self;

        self.HpState.RestoreHp(parameter.value);
    }
}
