using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMainManager : MonoSingleton<BattleMainManager>
{
    //managers
    [HideInInspector]
    public TurnPhaseManager turnPhaseManager;
    [HideInInspector]
    public BattleDeckManager battleDeckManager;
    [HideInInspector]
    public BattlePlayerDataManager battlePlayerDataManager;
    [Header("開場抽幾張卡")]
    public int initialDrawCard;

    void Start()
    {
        Initialmanagers();
        BattleStart();
    }
    //省呼叫資源
    private void Initialmanagers()
    {
        turnPhaseManager = TurnPhaseManager.instance;
        battlePlayerDataManager = BattlePlayerDataManager.instance;
        battleDeckManager = BattleDeckManager.instance;
    }

    public void BattleStart()
    {
        
        turnPhaseManager.StartGame();
        battleDeckManager.ShuffleDeck();
        battleDeckManager.DrawCard(initialDrawCard);

        battlePlayerDataManager.InitialPlayerStatus();
    }

}
