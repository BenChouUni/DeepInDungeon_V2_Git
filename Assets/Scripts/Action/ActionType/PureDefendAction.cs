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
        return string.Format("get {0} shield", parameter.value);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.Target == null) return;
        Character targetCharater = parameter.Target;
        //這邊要計算公式
        int shield = parameter.value;


        targetCharater.AddShield(shield);
    }
}