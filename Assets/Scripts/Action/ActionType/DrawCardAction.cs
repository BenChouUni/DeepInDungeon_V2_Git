using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardAction : CardActionBase
{
    public override ActionType type => ActionType.DrawCard;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("Draw {0} card",parameter.value);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        int num = parameter.value;
        Debug.LogFormat("抽{0}張牌", num);
        BattleDeckManager.instance.DrawCard(num);
    }
}
