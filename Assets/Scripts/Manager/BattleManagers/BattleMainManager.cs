using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //UI
    //public Button EndTurnButton;
   

    private void Awake()
    {
        //開場預設沒有拖拽東西
        isDragging = false;
        //button無法互動
        //EndTurnButton.interactable = false;
    }
    void Start()
    {
        Initialmanagers();
        
        
        BattleStart();
        //開始準備階段結束
        //玩家回合先開始
        turnPhaseManager.InitialTurn();
        
    }
    //省呼叫資源
    private void Initialmanagers()
    {
        //透過monosingleton找到每個管理器
        turnPhaseManager = TurnPhaseManager.instance;
        battlePlayerDataManager = BattlePlayerDataManager.instance;
        battleDeckManager = BattleDeckManager.instance;
        cardsLayoutManager = CardsLayoutManager.instance;
        
        
    }

    public void BattleStart()
    {
        //回合開始
        turnPhaseManager.StartGame();
        //洗牌
        battleDeckManager.ShuffleDeck();
        //抽牌
        battleDeckManager.DrawCard(initialDrawCard);
        //初始化玩家資料
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
            
            UseCard();
            cardsLayoutManager.SetLayout();
        }
    }

    /// <summary>
    /// 玩家使用卡牌
    /// </summary>
    private void UseCard()
    {
        CardData cardData = draggingCard.GetComponent<CardDisplay>().CardData;
        if (cardData.cost > battlePlayerDataManager.CurrentEnergy)
        {
            Debug.Log("費用不夠");
            return;
        }
        
        battlePlayerDataManager.ConsumeEnergy(cardData.cost);
        Debug.LogFormat("使用{0}", cardData.cardName);
        battleDeckManager.DisCard(cardData);
        
        cardsLayoutManager.RemoveHandCard(draggingCard.transform);
        Destroy(draggingCard);
        draggingCard = null;

        

    }
  

}
