using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleEnemyManager : MonoBehaviour, IDropHandler
{
    public EnemyData enemyData;

    public void OnDrop(PointerEventData eventData)
    {
        BattleMainManager.instance.DropRequest();
    }
}
