using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 單純獲得格擋值
/// </summary>
public class PureDefendAction : CardActionBase
{
    public override ActionType type => ActionType.PureDefend;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("獲得{0}點護甲", parameter.value);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.TargetList == null) return;

        foreach (Character item in parameter.TargetList)
        {
         
            item.AddShield(parameter.value);
        }
    }
}
