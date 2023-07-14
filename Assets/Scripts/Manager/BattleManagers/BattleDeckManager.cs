using System.Collections;
using System.Collections.Generic;
using System.Linq; //用來連結list的操作
using UnityEngine;

/// <summary>
/// 管理戰鬥發生的牌組互動，抽排洗牌棄牌等效果
/// </summary>
public class BattleDeckManager : MonoSingleton<BattleDeckManager>,IDataPersistence
{
    //數值
    [Header("手牌上限數量")]
    public int maxHandCards;
    //Prefab
    public GameObject CardPrefab;
    //Panel
    public Transform HandCardPanel;
    //cards
    public List<CardData> battleDeck;
    //public Dictionary<int,CardData>(;
    public List<CardData> handCards;
    public List<CardData> discardCards;
    public List<GameObject> handCardsObj;

    //CardZone
    public CardZoneDisplay drawCardZoneDisplay;
    public CardZoneDisplay discardZoneDisplay;

    //判斷
    private bool canDraw;
    

    public int HandCardCount
    {
        get;
    }

    private void Start()
    {
        canDraw = true;
        ShowCardZoneCount();
    }
    /// <summary>
    /// 牌組洗牌
    /// </summary>
    public void ShuffleDeck()
    {
        int len = battleDeck.Count;
        if ( len <= 1)
        {
            return;
        }

        for (int i = 0; i < len; i++)
        {
            CardData temp = battleDeck[i];
            int changeNum = Random.Range(i, len);
            battleDeck[i] = battleDeck[changeNum];
            battleDeck[changeNum] = temp;
        }

    }

    public void LoadData(GameData data)
    {

        //將兩武器的牌組合併成完整牌組
        battleDeck = new List<CardData>();
        //用迴圈避免用同一個ref而非複製
        foreach (CardData item in data.mainWeaponDeck)
        {
            battleDeck.Add(item);
        }
        foreach (CardData item in data.supWeaponDeck)
        {
            battleDeck.Add(item);
        }
    }

    public void SaveData(ref GameData data)
    {
        //戰鬥過程中應該無法存擋
    }

    public void ShowCardZoneCount()
    {
        int battleDeckCount = battleDeck.Count;
        drawCardZoneDisplay.ShowCounter(battleDeckCount);

        int discardCount = discardCards.Count;
        discardZoneDisplay.ShowCounter(discardCount);
    }
    /// <summary>
    /// 從抽牌庫抽卡
    /// </summary>
    /// <param name="num">數量</param>
    public void DrawCard(int num)
    {
        Debug.LogFormat("抽{0}",num);
        
        if (!canDraw)
        {
            Debug.Log("無法抽牌");
            return;
        }

        GameObject new_card;
        for (int i = 0; i < num; i++)
        {
            if (num > battleDeck.Count)
            {
                if (discardCards.Count == 0)
                {
                    Debug.Log("無牌可抽");
                    return;
                }
                Debug.Log("卡組數量不夠抽牌，重新把棄牌堆放回");
                RefreshDeck();

            }

            CardData cardData = battleDeck[0];//永遠取第一張，然後將其刪掉

            if (handCards.Count >= maxHandCards)
            {
                Debug.Log("抽牌超過手牌上限");
                //可能要把牌抽上來給玩家看一下
                //把牌一到棄排堆
                //DisCard(cardData);
                //handCards.Remove(cardData);
                discardCards.Add(cardData);
                //battleDeck.Remove(cardData);
                continue;

            }
             
            
            new_card = Instantiate(CardPrefab, HandCardPanel, false);
            new_card.GetComponent<CardDisplay>().CardData = cardData;
            handCards.Add(cardData);
            handCardsObj.Add(new_card);

            //counter



            battleDeck.RemoveAt(0);

            CardsLayoutManager.instance.AddHandCard(new_card.transform);
            
            
        }

        ShowCardZoneCount();
    }
    /// <summary>
    /// 刪除hand card obj
    /// </summary>
    /// <param name="card"></param>
    public void DisCard(GameObject card)
    {
        CardData cardData = card.GetComponent<CardDisplay>().CardData;
        //牌原本應該在手牌上，所以從手牌刪除，之後可能會有直接從牌庫刪除，需要修正
        handCards.Remove(cardData);
        CardsLayoutManager.instance.RemoveHandCard(card.transform);
        Destroy(card);
        handCardsObj.Remove(card);

        discardCards.Add(cardData);
        ShowCardZoneCount();
        
    }
    /// <summary>
    /// 把棄牌堆重新洗回抽排堆
    /// </summary>
    private void RefreshDeck()
    {
        foreach (CardData item in discardCards)
        {
            battleDeck.Add(item);
            
        }
        discardCards.Clear();
        ShuffleDeck();
    }
    /// <summary>
    /// 重新把手牌洗掉抽新的卡上來
    /// </summary>
    public void RefreshHandCards(int num)
    {
        Debug.Log("刷新手牌");
        for (int i = handCardsObj.Count -1; i >= 0; i--)
        {
            Debug.Log("刪除牌");
            DisCard(handCardsObj[i]);
        }
        handCardsObj.Clear();
        DrawCard(num);
    }
}
