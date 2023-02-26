using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ActionManager : MonoSingleton<ActionManager>
{
    public EnemyData enemyData;
    public PlayerData battleplayerData;
    private Character player;

    public void UseAction(CardAction action)
    {
        ActionType type = action.type;
        int parameter = action.parameter;
        TargetType target = action.target;
        switch (type)
        {
            case ActionType.Attack:
                AttackAction(GetCharacter(target), parameter);
                break;
            case ActionType.Defend:
                DefendAction(GetCharacter(target), parameter);
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
        Debug.LogFormat("對{0}造成{1}傷害", character.Name ,damage);
        character.GetDamage(damage);

    }

    public void TestDamage(int dmg)
    {
        
        CardAction action = new CardAction(ActionType.Attack, dmg,TargetType.Enemy);
        UseAction(action);
        EnemyManager.instance.UpdateEnemyStatus();
    }

    private void DefendAction(Character character, int shield)
    {
        Debug.LogFormat("{0}獲得{1}護盾", character.Name, shield);
        character.AddShield(shield);
    }

    public void TestDefend(int def)
    {
        
        CardAction action = new CardAction(ActionType.Defend, def,TargetType.Player);
        UseAction(action);
        //Debug.Log(battleplayerData.Shield);
        BattlePlayerDataManager.instance.UpdatePlayerStatus();
    }

    public void DrawCardAction(int num)
    {
        Debug.LogFormat("抽{0}張牌", num);
        BattleDeckManager.instance.DrawCard(num);
    }

    private Character GetCharacter(TargetType type)
    {
        if (type == TargetType.Player)
        {
            return BattlePlayerDataManager.instance.battleplayerData;
        }
        else if (type == TargetType.Enemy)
        {
            return EnemyManager.instance.enemyData;
        }
        else
        {
            return null;
        }
    }
}
