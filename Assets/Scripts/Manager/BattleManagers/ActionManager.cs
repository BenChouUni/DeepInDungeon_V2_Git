using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ActionManager : MonoSingleton<ActionManager>
{
    public EnemyData enemyData;
    public PlayerData battleplayerData;
    private Character player;


    //現在打出卡牌的武器
    [SerializeField]
    private WeaponData actioningWeapon;
    /// <summary>
    /// 使用卡牌當中所有效果
    /// </summary>
    /// <param name="cardData"></param>
    public void UseCardAllAction(CardData cardData)
    {
        if (TurnPhaseManager.instance.GamePhase == GamePhase.GameEnd)
        {
            return;
        }
        List<CardAction> actions = cardData.cardAction;
        actioningWeapon = cardData.WeaponData;

        foreach (CardAction action in actions)
        {
            DoAction(action);
        }
        
    }

    private void DoAction(CardAction action)
    {
        ActionType type = action.type;
        int parameter = action.parameter;
        TargetType target = action.target;
        Character character = GetCharacter(target);
        switch (type)
        {
            case ActionType.DealDamage:
                DealDamageAction(character, parameter);
                break;
            case ActionType.Defend:
                DefendAction(character, parameter);
                break;
            case ActionType.DrawCard:
                DrawCardAction(parameter);
                break;
            case ActionType.Heal:
                HealAction(character, parameter);
                break;
            case ActionType.RealDefend:
                RealDefendAction(character, parameter);
                break;
            default:
                break;
        }
    }

    private void DealDamageAction(Character character, int parameter)
    {
        int damage = parameter + actioningWeapon.atk;

        Debug.LogFormat("對{0}造成{1}傷害", character.Name ,damage);

        character.GetDamage(damage);

    }

    private void DefendAction(Character character, int parameter)
    {

        int shield = parameter + actioningWeapon.def;

        Debug.LogFormat("{0}獲得{1}護盾", character.Name, shield);
        character.AddShield(shield);
    }


    public void DrawCardAction(int num)
    {
        Debug.LogFormat("抽{0}張牌", num);
        BattleDeckManager.instance.DrawCard(num);
    }

    private void HealAction(Character character, int num)
    {
        Debug.LogFormat("{1}回{0}血", num,character.Name);
        character.RestoreHealth(num);
    }

    private void RealDefendAction(Character character, int parameter)
    {

        int shield = parameter;

        Debug.LogFormat("{0}獲得{1}護盾", character.Name, shield);
        character.AddShield(shield);
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
    #region
    public void TestDamage(int dmg)
    {

        CardAction action = new CardAction(ActionType.DealDamage, dmg, TargetType.Enemy);
        DoAction(action);
        EnemyManager.instance.UpdateEnemyStatus();
    }
    public void TestDefend(int def)
    {

        CardAction action = new CardAction(ActionType.Defend, def, TargetType.Player);
        DoAction(action);
        BattlePlayerDataManager.instance.UpdatePlayerStatus();
    }
    public void TestRealDefend(int def)
    {

        CardAction action = new CardAction(ActionType.RealDefend, def, TargetType.Enemy);
        DoAction(action);
        EnemyManager.instance.UpdateEnemyStatus();
    }
    #endregion
}
