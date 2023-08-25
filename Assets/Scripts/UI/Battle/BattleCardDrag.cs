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
            Debug.Log("開始拉牌，停止Move");
            this.GetComponent<CardMoveUI>().stop_Move();
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
        //拖移時卡片更隨
        //this.transform.position = eventData.position;

        //呼叫Arrow
        //this.transform.position = new Vector2(960f, 200f);
        //this.transform.position = new Vector3(960f, 200f, eventData.position.z);
        //this.transform.position = new Vector3(960f, 200f, 5f);
        CardsLayoutManager.instance.arrow.GetComponent<BezierArrows>().Show(this.GetComponent<RectTransform>());
        this.transform.rotation = (Quaternion.Euler(0f, 0f, 0f));
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

        
        //解除鎖定卡牌，手牌可繼續移動
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
