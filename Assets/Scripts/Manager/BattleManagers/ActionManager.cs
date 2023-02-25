using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoSingleton<ActionManager>
{
    public EnemyData enemyData;
    public PlayerData battleplayerData;
    

    public void UseAction(CardAction action, Character character)
    {
        ActionType type = action.type;
        int parameter = action.parameter;
        switch (type)
        {
            case ActionType.Attack:
                AttackAction(character, parameter);
                break;
            case ActionType.Defend:
                DefendAction(character, parameter);
                break;
            case ActionType.DrawCard:
                DrawCardAction(parameter);
                break;
            default:
                break;
        }
    }

    private void AttackAction(Character character, int damage)
    {
        character.GetDamage(damage);

    }

    public void TestDamage(int dmg)
    {
        enemyData = EnemyManager.instance.enemyData;
        CardAction action = new CardAction(ActionType.Attack, dmg);
        UseAction(action, enemyData);
        EnemyManager.instance.ShowEnemy();
    }

    private void DefendAction(Character character, int shield)
    {
        character.AddShield(shield);
    }

    public void TestDefend(int def)
    {
        battleplayerData = BattlePlayerDataManager.instance.battleplayerData;
        CardAction action = new CardAction(ActionType.Defend, def);
        UseAction(action, battleplayerData);
        //Debug.Log(battleplayerData.Shield);
        BattlePlayerDataManager.instance.ShowShield();
    }

    public void DrawCardAction(int num)
    {
        BattleDeckManager.instance.DrawCard(num);
    }
}
