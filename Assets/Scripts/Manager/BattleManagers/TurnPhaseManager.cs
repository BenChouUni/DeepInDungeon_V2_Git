using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    GameStart,PlayerDraw,PlayerAction,EnemyAction,GameEnd
}

public class TurnPhaseManager : MonoSingleton<TurnPhaseManager>
{
    
    public GamePhase gamePhase;

    public void StartGame()
    {
        gamePhase = GamePhase.GameStart;
    }


    public void EndPlayerTurn()
    {
        gamePhase = GamePhase.EnemyAction;
    }
}
