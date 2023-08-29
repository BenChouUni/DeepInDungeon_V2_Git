using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyDetecter : MonoBehaviour, IDropHandler
{
    private EnemyData enemyData = null;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop card");
        //BattleMainManager.instance.DropRequest();
    }
}
