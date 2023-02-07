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
        
        Vector3 move = this.transform.position;
        move = new Vector3(move.x, move.y + UP, move.z);
        this.transform.position = move;
        CardsLayoutManager.instance.Disperse(this.transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 move = this.transform.position;
        move = new Vector3(move.x, move.y - UP, move.z);
        this.transform.position = move;
        CardsLayoutManager.instance.Gather(this.transform.position);
    }
}
