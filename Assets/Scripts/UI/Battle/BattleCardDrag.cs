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
        if (!DropGoBack)
        {
            parentReturnTo = this.transform.parent;

            battleMainManager.StartDrag(this.gameObject);
            TurnRaycastBlock(false);

            CardsLayoutManager.instance.CanInPointer = false;
            CardsLayoutManager.instance.Nowdragging = true;
            this.GetComponent<CardMoveUI>().bedragging = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!DropGoBack)
        {
            this.transform.position = eventData.position;
            this.transform.rotation = (Quaternion.Euler(0f, 0f, 0f));

            CardsLayoutManager.instance.CanInPointer = false;
            CardsLayoutManager.instance.Nowdragging = true;
            this.GetComponent<CardMoveUI>().bedragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!DropGoBack)
        {
            battleMainManager.EndDrag();
            this.transform.SetParent(parentReturnTo);


            TurnRaycastBlock(true);

            CardsLayoutManager.instance.CanInPointer = true;
            this.GetComponent<CardMoveUI>().bedragging = false;
            CardsLayoutManager.instance.CardGoBack++;
            CardsLayoutManager.instance.Nowdragging = false;
            DropGoBack = true;
        }
    }

    private void TurnRaycastBlock(bool value)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = value;
    }
}
