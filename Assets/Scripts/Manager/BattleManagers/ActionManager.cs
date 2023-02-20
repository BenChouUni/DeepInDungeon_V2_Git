using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoSingleton<ActionManager>
{
    public EnemyData enemyData;
    public PlayerData battleplayerData;

    public void UseAction(Action action, Character character)
    {
        int id = action.id;
        int parameter = action.parameter;
        switch (id)
        {
            case 0:
                AttackAction(character, parameter);
                break;
            case 1:
                DefendAction(character, parameter);
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
        Action action = new Action(0, dmg);
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
        Action action = new Action(1, def);
        UseAction(action, battleplayerData);
        //Debug.Log(battleplayerData.Shield);
        BattlePlayerDataManager.instance.ShowShield();
    }
}
