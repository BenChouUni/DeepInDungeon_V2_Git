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
    [HideInInspector]
    public ActionManager actionManager;
    [HideInInspector]
    public EnemyManager enemyManager;
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
        actionManager = ActionManager.instance;
        enemyManager = EnemyManager.instance;
        
        
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
        // 判定現在回合階段
        if (turnPhaseManager.GamePhase != GamePhase.PlayerAction)
        {
            Debug.Log("不是玩家回合無法使用卡牌");
            return;
        }
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
        //消耗費用
        battlePlayerDataManager.ConsumeEnergy(cardData.cost);
        //使用牌
        Debug.LogFormat("使用{0}", cardData.cardName);
        foreach (CardAction action in cardData.cardAction)
        {
            actionManager.UseAction(action);
        }
        UpdateUI();
        //將牌刪除
        battleDeckManager.DisCard(cardData);
        cardsLayoutManager.RemoveHandCard(draggingCard.transform);
        Destroy(draggingCard);
        draggingCard = null;

        

    }
    /// <summary>
    /// 更新我方與敵人的狀態顯示
    /// </summary>
    private void UpdateUI()
    {
        battlePlayerDataManager.UpdatePlayerStatus();
        enemyManager.UpdateEnemyStatus();
    }
    /// <summary>
    /// 每個回合開始呼叫，執行回合開始所需的初始動作
    /// </summary>
    public void TurnStart()
    {
        
        if (turnPhaseManager.GamePhase == GamePhase.PlayerAction)
        {
            Debug.Log("執行玩家回合開始準備");
        }
        else if (turnPhaseManager.GamePhase == GamePhase.EnemyAction)
        {
            Debug.Log("執行敵人回合開始準備");
        }
    }

}
