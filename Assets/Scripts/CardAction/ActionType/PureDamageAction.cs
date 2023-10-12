using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 不透過武器的純粹傷害
/// </summary>
public class PureDamageAction : CardActionBase
{
    public override ActionType type => ActionType.PureDamage;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("直接向{0}造成{1}點傷害",parameter.targetType ,parameter.value);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.TargetList == null) return;

        foreach (Character item in parameter.TargetList)
        {

            item.GetDamage(parameter.value);
        }
    }
}
