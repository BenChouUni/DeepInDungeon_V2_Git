using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomInUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(1,3)]
    public float ZoomSize;

    //private Transform parentReturnTo = null;//要返回的位置的parent
    //private GameObject canvas;
    private void Start()
    {
        //canvas = GameObject.Find("Canvas").gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //parentReturnTo = this.transform.parent;
        //this.transform.SetParent(canvas.transform);
        this.transform.localScale = Vector2.one * ZoomSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = Vector2.one;
        //this.transform.SetParent(parentReturnTo);
    }
}
