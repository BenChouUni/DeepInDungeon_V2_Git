using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponGoBack : MonoBehaviour, IDropHandler
{
    public Transform WeaponPanel;    //�]�m�d���Ӧ^�h��parant

    public void OnDrop(PointerEventData eventData)
    {
        GameObject GobackItem = eventData.pointerDrag;
        //Debug.Log("DropNow");

        GoBackWeapon(GobackItem);

    }
    
    public void GoBackWeapon(GameObject GoBackCard)
    {
        //Debug.Log("I want GO back");

        //��^�Z��
        if (GoBackCard.TryGetComponent(out DragCard dragCard))
        {
            dragCard.parentReturnTo = this.WeaponPanel.transform;

        }
    }

}
