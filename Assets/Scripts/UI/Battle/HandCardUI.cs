using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(1, 300)]
    public float UP = 150;

    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointer");
        CardsLayoutManager.instance.Disperse(this.transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardsLayoutManager.instance.Gather(this.transform.position);
    }
}
