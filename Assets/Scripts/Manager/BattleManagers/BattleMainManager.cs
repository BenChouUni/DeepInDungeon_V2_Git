using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    //[HideInInspector]
    //public ActionManager actionManager;
    [HideInInspector]
    public EnemyManager enemyManager;
   

    [Header("開場抽幾張卡")]
    public int initialDrawCard;
    //drag drop
    private bool isDragging = false;
    private GameObject draggingCard;
    //UI
    [Header("傷害數字顯示")]
    public GameObject HitNumber;

    
   

    private void Awake()
    {
        Initialmanagers();
        //開場預設沒有拖拽東西
        isDragging = false;
        //button無法互動
        //EndTurnButton.interactable = false;
    }
    void Start()
    {

       
        StartBattle();
        //開始準備階段結束
        //玩家回合先開始
        turnPhaseManager.InitialTurn();
        
        
    }
    /*特殊使用，以後可能會更改*/
    /// <summary>
    /// 給ActionParameter調用Character資料使用
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Character GetCharacterByType(CharaterType type)
    {
        if (type == CharaterType.Enemy)
        {
            if(enemyManager == null)
            {
                Debug.LogError("enemyData空");
            }
            return enemyManager.enemyData;
        }
        else
        {
            if (battlePlayerDataManager == null)
            {
                Debug.LogError("battlePlayerData空");
            }
            return battlePlayerDataManager.GetPlayerData();
        }
    }
    //省呼叫資源
    private void Initialmanagers()
    {
        //透過monosingleton找到每個管理器
        turnPhaseManager = TurnPhaseManager.instance;
        battlePlayerDataManager = BattlePlayerDataManager.instance;
        battleDeckManager = BattleDeckManager.instance;
        cardsLayoutManager = CardsLayoutManager.instance;
        //actionManager = ActionManager.instance;
        enemyManager = EnemyManager.instance;
        
        
    }
    //開始戰鬥
    public void StartBattle()
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

    //戰鬥結束
    public void EndBattle()
    {
        Debug.Log("戰鬥結束");
        turnPhaseManager.EndBattle();

    }

    public void WinBattle()
    {
        EndBattle();
        Debug.Log("戰鬥勝利");
        AwardMainManager.instance.ShowAwardScene();
        //跳轉到獎勵卡環節
        //儲存現在血量
        battlePlayerDataManager.saveCurrentHealth();
        DataPersistenceManager.instance.SaveGame();
    }

    public void LoseBattle()
    {
        EndBattle();
        Debug.Log("戰鬥失敗");
        //跳轉回武器選擇畫面
        AwardMainManager.instance.MapData.Reset();
        battlePlayerDataManager.ResetPlayerData();
        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadScene(0);

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
            CardsLayoutManager.instance.Nowdragging = false;
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
        //actionManager.UseCardAllAction(cardData);
        foreach (CardActionSet item in cardData.cardActions)
        {
            Debug.Log(item.actionType);
            item.DoAction();
        }
        //UpdateStatus();
        
        

        //將牌刪除
        battleDeckManager.DisCard(draggingCard);
        //cardsLayoutManager.RemoveHandCard(draggingCard.transform);
        //Destroy(draggingCard);
        draggingCard = null;

        

    }
    private IEnumerator EnemyBehave()
    {
        //敵人行動
        Debug.Log("敵人行動");
        enemyManager.DoEnemyAction();
        //更新狀態
        //UpdateStatus();
        yield return new WaitForSeconds(2);
        //turn end
        turnPhaseManager.EndTurn();
    }
    /// <summary>
    /// 檢視死亡與否以及更新我方與敵人的狀態顯示，死亡則直接進入結束遊戲
    /// </summary>
    //private void UpdateStatus()
    //{
    //    battlePlayerDataManager.UpdatePlayerStatus();
    //    enemyManager.UpdateEnemyStatus();
    //    if (battlePlayerDataManager.battleplayerData.isDeath || enemyManager.enemyData.isDeath)
    //    {
    //        StartCoroutine(EndBattle());
    //    }
    //    if (enemyManager.enemyData.isDeath)
    //    {
    //        AwardMainManager.instance.ShowAwardScene();
    //    }
    //}
    /// <summary>
    /// 每個回合開始呼叫，執行回合開始所需的初始動作
    /// </summary>
    public void TurnStart()
    {
        
        if (turnPhaseManager.GamePhase == GamePhase.PlayerAction)
        {
            Debug.Log("執行玩家回合開始準備");
            battlePlayerDataManager.ResetEnergy();
            battleDeckManager.DrawCard(initialDrawCard);

        }
        else if (turnPhaseManager.GamePhase == GamePhase.EnemyAction)
        {
            Debug.Log("執行敵人回合開始準備");
            StartCoroutine(EnemyBehave());
        }
    }
    public void TurnEnd()
    {
        if (turnPhaseManager.GamePhase == GamePhase.PlayerAction)
        {
            Debug.Log("玩家回合結束");
            //battlePlayerDataManager.ResetEnergy();
            battleDeckManager.RefreshHandCards();
            foreach (StateEffect item in battlePlayerDataManager.battleplayerData.StateList)
            {
                item.AtTurnEnd();
            }

        }
        else if (turnPhaseManager.GamePhase == GamePhase.EnemyAction)
        {
            Debug.Log("敵人回合結束");
            foreach (var item in enemyManager.enemyData.StateList)
            {
                item.AtTurnEnd();
            }

        }
        //在PhaseManager那邊切換
    }
    public void GenerateHitNum(int num,Transform transform)
    {
        var numberObj = Instantiate(HitNumber, transform);
        numberObj.GetComponentInChildren<Text>().text = num.ToString();
    }
}
