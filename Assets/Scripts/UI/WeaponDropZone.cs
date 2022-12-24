using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropItem = eventData.pointerDrag;
        
        if (dropItem.TryGetComponent(out DragCard dragCard))
        {
            dragCard.parentReturnTo = this.transform;
        }
    }
}
