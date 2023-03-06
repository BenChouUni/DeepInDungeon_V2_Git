using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentReturnTo = null;

    private BattleMainManager battleMainManager;

    public bool DropGoBack = false;

    private void Start()
    {
        battleMainManager = BattleMainManager.instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentReturnTo = this.transform.parent;

        battleMainManager.StartDrag(this.gameObject);
        TurnRaycastBlock(false);

        CardsLayoutManager.instance.CanInPointer = false;
        this.GetComponent<CardMoveUI>().bedragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        this.transform.rotation = (Quaternion.Euler(0f, 0f, 0f));

        CardsLayoutManager.instance.CanInPointer = false;
        this.GetComponent<CardMoveUI>().bedragging= true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        battleMainManager.EndDrag();
        this.transform.SetParent(parentReturnTo);
        

        TurnRaycastBlock(true);

        CardsLayoutManager.instance.CanInPointer = true;
        this.GetComponent<CardMoveUI>().bedragging = false;
        CardsLayoutManager.instance.CardGoBack ++;
        DropGoBack = true;
    }

    private void TurnRaycastBlock(bool value)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = value;
    }
}
