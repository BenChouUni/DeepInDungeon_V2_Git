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
        List<CardActionBase> actions = cardData.cardAction;
        actioningWeapon = cardData.WeaponData;

        foreach (CardActionBase action in actions)
        {
            DoAction(action);
        }
        
    }

    public void DoAction(CardActionBase action)
    {
        ActionType type = action.type;
        int parameter = action.parameter;
        TargetType target = action.target;
        StatusEffect statusEffect = EffectFactory.GetStatusEffect(action.StatusEffect);
        Character character = GetCharacter(target);

        switch (type)
        {
            case ActionType.WeaponAttack:
                WeaponAttackAction(character, parameter);
                break;
            case ActionType.WeaponDefend:
                WeaponDefendAction(character, parameter);
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
            case ActionType.Give:
                GiveAction(target, parameter, statusEffect);
                break;
            default:
                break;
        }
    }

    //給予狀態
    private void GiveAction(TargetType targetType, int parameter,StatusEffect statusEffect)
    {
        Debug.LogFormat("給予{0}狀態{1}", targetType,statusEffect.effectName);
        StatusEffectManager statusEffectManager = StatusEffectManager.instance;
        statusEffectManager.AddEffect(targetType, statusEffect, parameter);
        statusEffectManager.ShowEffect();
    }

    //純粹傷害，基本傷害計算透過這裡
    private void PureDamageAction(Character character, int parameter)
    {
        float damage = parameter;
        List<StatusEffect> effects = StatusEffectManager.instance.GetTargetEffectList(character.targetType);
        //根據效果執行傷害增幅
        foreach (StatusEffect item in effects)
        {
           damage *= item.AtReceiveDamage();
        }

        Debug.LogFormat("造成{0}傷害", damage);

        character.GetDamage((int)damage);
    }

    private void PureDefendAction(Character character, int parameter)
    {

        int shield = parameter;

        Debug.LogFormat("獲得{0}護盾", shield);
        character.AddShield(shield);
    }
    /// <summary>
    /// 造成武器+n傷害
    /// </summary>
    /// <param name="character"></param>
    private void WeaponAttackAction(Character character,int parameter)
    {
        int damage =actioningWeapon.atk + parameter;

        Debug.LogFormat("使用{0}攻擊", actioningWeapon.weaponName);
        this.PureDamageAction(character, damage);
        //character.GetDamage(damage);

    }

    private void WeaponDefendAction(Character character,int parameter)
    {

        int shield = actioningWeapon.def + parameter;

        Debug.LogFormat("使用{0}", actioningWeapon.weaponName);
        this.PureDefendAction(character, shield);
        //character.AddShield(shield);
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

        CardActionBase action = new CardActionBase(ActionType.WeaponAttack, dmg, TargetType.Enemy);
        DoAction(action);
        EnemyManager.instance.UpdateEnemyStatus();
    }
    public void TestDefend(int def)
    {

        CardActionBase action = new CardActionBase(ActionType.WeaponDefend, def, TargetType.Player);
        DoAction(action);
        BattlePlayerDataManager.instance.UpdatePlayerStatus();
    }
    public void TestRealDefend(int def)
    {

        CardActionBase action = new CardActionBase(ActionType.RealDefend, def, TargetType.Enemy);
        DoAction(action);
        EnemyManager.instance.UpdateEnemyStatus();
    }
    #endregion
}
