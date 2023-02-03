using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMainManager : MonoSingleton<BattleMainManager>
{
    [Header("開場抽幾張卡")]
    public int initialDrawCard;

    void Start()
    {
        BattleStart();
    }

    public void BattleStart()
    {
        
        TurnPhaseManager.instance.StartGame();
        BattleDeckManager.instance.ShuffleDeck();
        BattleDeckManager.instance.DrawCard(initialDrawCard);
    }

}
