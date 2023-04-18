using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class CardActionFactory
{
    public static CardActionBase GetCardAction(ActionType type)
    {
        CardActionBase cardActionBase = null;
        switch (type)
        {
            case ActionType.PureDamage:
                cardActionBase = new PureDamageAction();
                break;
            case ActionType.PureDefend:
                cardActionBase = new PureDefendAction();
                break;
            case ActionType.WeaponAttack:
                cardActionBase = new WeaponAttackAction();
                break;
            case ActionType.WeaponDefend:
                cardActionBase = new WeaponDefendAction();
                break;
            case ActionType.DrawCard:
                cardActionBase = new DrawCardAction();
                break;
            case ActionType.Heal:
                cardActionBase = new HealAction();
                break;
            case ActionType.Give:
                cardActionBase = new GiveStateAction();
                break;
            
            
        }
        return cardActionBase;
    }
}
