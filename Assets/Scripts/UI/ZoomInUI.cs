using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomInUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(1,3)]
    public float ZoomSize;

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = Vector3.one * ZoomSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = Vector3.one;
    }
}
