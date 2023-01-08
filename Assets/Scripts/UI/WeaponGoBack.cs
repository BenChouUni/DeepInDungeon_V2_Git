using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponGoBack : MonoBehaviour, IDropHandler
{
    public Transform WeaponPanel;    //設置卡片該回去的parant

    public void OnDrop(PointerEventData eventData)
    {
        GameObject GobackItem = eventData.pointerDrag;
        //Debug.Log("DropNow");

        GoBackWeapon(GobackItem);

    }
    
    public void GoBackWeapon(GameObject GoBackCard)
    {
        //Debug.Log("I want GO back");

        //放回武器
        if (GoBackCard.TryGetComponent(out DragCard dragCard))
        {
            dragCard.parentReturnTo = this.WeaponPanel.transform;

        }
    }

}
