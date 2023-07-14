using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GamePhase
{
    GameStart,PlayerAction,EnemyAction,GameEnd
}

public class TurnPhaseManager : MonoSingleton<TurnPhaseManager>
{
    
    private GamePhase gamePhase;
    public GamePhase GamePhase
    {
        get { return gamePhase; }
    }

    private int TurnCount;
    public Text TurnDisplay;

    public void StartGame()
    {
        gamePhase = GamePhase.GameStart;
        ShowTurn();
    }
    /// <summary>
    /// 準備階段結束後開始初始回合
    /// </summary>
    public void InitialTurn()
    {
        TurnCount = 0;
        gamePhase = GamePhase.PlayerAction;
        ShowTurn();
    }
    public void EndPlayerTurn()
    {
        if (gamePhase == GamePhase.PlayerAction)
        {
            BattleMainManager.instance.TurnEnd();
            gamePhase = GamePhase.EnemyAction;
            BattleMainManager.instance.TurnStart();
        }
        else
        {
            Debug.Log("cannot end player turn right now");
        }
        ShowTurn();
        return;
    }

    public void EndEnemyTurn()
    {
        if (gamePhase == GamePhase.EnemyAction)
        {
            gamePhase = GamePhase.PlayerAction;
            TurnCount++;
            BattleMainManager.instance.TurnStart();
        }
        else
        {
            Debug.Log("cannot end enenmy turn right now");
        }
        ShowTurn();
        return;
    }

    public void EndBattle()
    {
        gamePhase = GamePhase.GameEnd;
        ShowTurn();
    }

    private void ShowTurn()
    {
        TurnDisplay.text = gamePhase.ToString();
    }

    public void EndTurn()
    {
        BattleMainManager.instance.TurnEnd();
        //切換回合
        if (gamePhase == GamePhase.PlayerAction)
        {
            gamePhase = GamePhase.EnemyAction;
            BattleMainManager.instance.TurnStart();
        }
        else if (gamePhase == GamePhase.EnemyAction)
        {
            gamePhase = GamePhase.PlayerAction;
            TurnCount++;
            BattleMainManager.instance.TurnStart();
        }
        ShowTurn();
    }
}
