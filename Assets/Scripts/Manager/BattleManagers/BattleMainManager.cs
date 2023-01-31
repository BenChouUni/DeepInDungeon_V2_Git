using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMainManager : MonoSingleton<BattleMainManager>
{

    void Start()
    {
        
    }

    public void BattleStart()
    {
        TurnPhaseManager.instance.StartGame();
    }

}
