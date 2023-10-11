using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class BattleCardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentReturnTo = null;

    private BattleMainManager battleMainManager;

    //public bool DropGoBack = false;

    private void Start()
    {
        battleMainManager = BattleMainManager.instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentReturnTo = this.transform.parent;

        if(this.GetComponent<HandCardUI>() != null)
        {
            //Debug.Log("OnBeginDrag");
            this.GetComponent<CardMoveUI>().stop_Move();    //停止所有正在移動的卡牌
            CardsLayoutManager.instance.locking_Card();
            //this.GetComponent<HandCardUI>().enabled = false;
        }

        battleMainManager.StartDrag(this.gameObject);
        TurnRaycastBlock(false);

        /*
        if (!DropGoBack)
        {
            parentReturnTo = this.transform.parent;

            battleMainManager.StartDrag(this.gameObject);
            TurnRaycastBlock(false);

            CardsLayoutManager.instance.CanInPointer = false;
            CardsLayoutManager.instance.Nowdragging = true;
            this.GetComponent<CardMoveUI>().bedragging = false;
        }
        */
    }

    public void OnDrag(PointerEventData eventData)
    {
        //�첾�ɥd�����H
        CardData cardData = this.GetComponent<CardDisplay>().CardData;
        if (cardData.isAimable())
        {
            CardsLayoutManager.instance.arrow.GetComponent<BezierArrows>().Show(this.GetComponent<RectTransform>());
            this.transform.rotation = (Quaternion.Euler(0f, 0f, 0f));
        }
        else
        {
            this.transform.position = eventData.position;
        }
        //this.transform.position = eventData.position;

        //�I�sArrow
        
        //CardsLayoutManager.instance.arrow.GetComponent<BezierArrows>().Show(this.GetComponent<RectTransform>());
        //this.transform.rotation = (Quaternion.Euler(0f, 0f, 0f));
        /*
        if (!DropGoBack)
        {
            this.transform.position = eventData.position;
            this.transform.rotation = (Quaternion.Euler(0f, 0f, 0f));

            CardsLayoutManager.instance.CanInPointer = false;
            CardsLayoutManager.instance.Nowdragging = true;
            this.GetComponent<CardMoveUI>().bedragging = true;
        }
        */
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        CardsLayoutManager.instance.arrow.GetComponent<BezierArrows>().Hide();
        battleMainManager.EndDrag();
        this.transform.SetParent(parentReturnTo);

        
        //�Ѱ���w�d�P�A��P�i�~�򲾰�
        CardsLayoutManager.instance.cancel_Lock();

        TurnRaycastBlock(true);

        /*if (!DropGoBack)
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
        */
    }

    private void TurnRaycastBlock(bool value)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = value;
    }
}
