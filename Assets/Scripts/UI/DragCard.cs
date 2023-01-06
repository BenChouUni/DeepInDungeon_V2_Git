using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用event system
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //初始parent 以便放開後返回

    private Transform StartParent;
    [HideInInspector]
    public Transform parentReturnTo = null;//要返回的位置的parent


    public void OnBeginDrag(PointerEventData eventData)//開始拖動執行一次
    {
        StartParent = this.transform.parent;
        parentReturnTo = this.transform.parent;

        //拖動時移到canvas底下
        this.transform.SetParent(transform.root);
        transform.SetAsLastSibling(); //移動到最下層

        //關閉Raycast Block
        TurnRaycastBlock(false);
    }


    public void OnDrag(PointerEventData eventData)//拖動中
    {
        this.transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentReturnTo);

        //重新打開raycast的影響
        TurnRaycastBlock(true);
    }
    /// <summary>
    /// 返回初始所在
    /// </summary>
    public void ReturnToStartParent()
    {
        this.transform.SetParent(StartParent);
        parentReturnTo = StartParent;
    }

    private void TurnRaycastBlock(bool value)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = value;
    }
}

