using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
/// <summary>
/// 使用防禦力進行攻擊
/// </summary>
public class ShieldDamageAction : CardActionBase
{
    public override ActionType type => ActionType.ShieldDamage;

    public override string ActionDescribe(CardActionParameter parameter)
    {
        return string.Format("向{0}造成{1}點防禦傷害", parameter.targetType, parameter.Self.Shield);
    }

    public override void DoAction(CardActionParameter parameter)
    {
        //如果沒有目標就直接跳出
        if (parameter.TargetList == null) return;
        foreach (Character target in parameter.TargetList)
        {
            Character targetCharater = target;
            List<StateEffect> targetStateList = targetCharater.StateList;

            Character selCharacter = parameter.Self;
            List<StateEffect> myStateList = selCharacter.StateList;


            //這邊要計算公式
            float damagef = (parameter.Self.Shield);

            int damage = (int)damagef;
            //Debug.Log(damage);

            targetCharater.GetDamage(damage);
        }

    }
}
