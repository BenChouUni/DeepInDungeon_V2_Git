using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentReturnTo = null;

    private BattleMainManager battleMainManager;

    private void Start()
    {
        battleMainManager = BattleMainManager.instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentReturnTo = this.transform.parent;
        

        battleMainManager.StartDrag(this.gameObject);
        TurnRaycastBlock(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        battleMainManager.EndDrag();
        this.transform.SetParent(parentReturnTo);
        

        TurnRaycastBlock(true);

    }

    private void TurnRaycastBlock(bool value)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = value;
    }
}
