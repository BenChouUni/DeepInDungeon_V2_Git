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
            case ActionType.WeaponAttack:
                WeaponAttackAction(character);
                break;
            case ActionType.WeaponDefend:
                WeaponDefendAction(character);
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
            case ActionType.PureDamage:
                PureDamageAction(character, parameter);
                break;
            case ActionType.PureDefend:
                PureDefendAction(character, parameter);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 造成武器傷害
    /// </summary>
    /// <param name="character"></param>
    private void WeaponAttackAction(Character character)
    {
        int damage =actioningWeapon.atk;

        Debug.LogFormat("使用{0}攻擊，造成{1}傷害", actioningWeapon.weaponName,damage);

        character.GetDamage(damage);

    }

    private void WeaponDefendAction(Character character)
    {

        int shield = actioningWeapon.def;

        Debug.LogFormat("使用{0}獲得{1}護盾", actioningWeapon.weaponName, shield);
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

    private void PureDamageAction(Character character,int parameter)
    {
        int damage = parameter;

        Debug.LogFormat("造成{0}傷害", damage);

        character.GetDamage(damage);
    }

    private void PureDefendAction(Character character,int parameter)
    {

        int shield = parameter;

        Debug.LogFormat("獲得{0}護盾", shield);
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

        CardAction action = new CardAction(ActionType.WeaponAttack, dmg, TargetType.Enemy);
        DoAction(action);
        EnemyManager.instance.UpdateEnemyStatus();
    }
    public void TestDefend(int def)
    {

        CardAction action = new CardAction(ActionType.WeaponDefend, def, TargetType.Player);
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
