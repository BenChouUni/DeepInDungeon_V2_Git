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
        if (CardsLayoutManager.instance.CanInPointer)
        {
            CardsLayoutManager.instance.Disperse(this.transform.position);
        }
        else if (CardsLayoutManager.instance.Dispersenow || CardsLayoutManager.instance.Gathernow)
        {
            CardsLayoutManager.instance.Disperse(this.transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit");
        if(CardsLayoutManager.instance.CanInPointer || CardsLayoutManager.instance.Dispersenow)
        {
            CardsLayoutManager.instance.Gather(this.transform.position);
        }
        //CardsLayoutManager.instance.Gather(this.transform.position);
    }
}
