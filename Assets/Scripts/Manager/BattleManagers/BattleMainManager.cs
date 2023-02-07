using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMainManager : MonoSingleton<BattleMainManager>
{
    //managers
    [HideInInspector]
    public TurnPhaseManager turnPhaseManager;
    [HideInInspector]
    public BattleDeckManager battleDeckManager;
    [HideInInspector]
    public BattlePlayerDataManager battlePlayerDataManager;
    [HideInInspector]
    public CardsLayoutManager cardsLayoutManager;
    [Header("開場抽幾張卡")]
    public int initialDrawCard;
    //drag drop
    private bool isDragging;
    private GameObject draggingCard;
    void Start()
    {
        Initialmanagers();
        BattleStart();
    }
    //省呼叫資源
    private void Initialmanagers()
    {
        turnPhaseManager = TurnPhaseManager.instance;
        battlePlayerDataManager = BattlePlayerDataManager.instance;
        battleDeckManager = BattleDeckManager.instance;
        cardsLayoutManager = CardsLayoutManager.instance;

        isDragging = false;
    }

    public void BattleStart()
    {
        
        turnPhaseManager.StartGame();
        battleDeckManager.ShuffleDeck();
        battleDeckManager.DrawCard(initialDrawCard);

        battlePlayerDataManager.InitialPlayerStatus();
    }
    /// <summary>
    /// 卡牌拖拽時追蹤
    /// </summary>
    /// <param name="go">被拖墜卡牌本身</param>
    public void StartDrag(GameObject go)
    {
        this.draggingCard = go;
        isDragging = true;
        cardsLayoutManager.SetLayout();
    }
    public void EndDrag()
    {
        isDragging = false;
        this.draggingCard = null;
        cardsLayoutManager.SetLayout();
    }
    public void DropRequest()
    {
        if (draggingCard.TryGetComponent<BattleCardDrag>(out BattleCardDrag dragCard))
        {
            CardData cardData = draggingCard.GetComponent<CardDisplay>().CardData;
            Debug.LogFormat("使用{0}", cardData.cardName);
            battleDeckManager.DisCard(cardData);
            Destroy(draggingCard);
            draggingCard = null;
            cardsLayoutManager.SetLayout();
        }
    }

}
