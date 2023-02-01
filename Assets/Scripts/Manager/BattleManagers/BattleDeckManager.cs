using System.Collections;
using System.Collections.Generic;
using System.Linq; //用來連結list的操作
using UnityEngine;

/// <summary>
/// 管理戰鬥發生的牌組互動，抽排洗牌棄牌等效果
/// </summary>
public class BattleDeckManager : MonoSingleton<BattleDeckManager>,IDataPersistence
{
    //Prefab
    public GameObject CardPrefab;
    //Panel
    public Transform HandCardPanel;
    //cards
    public List<CardData> battleDeck;
    public List<CardData> handCards;
    public List<CardData> discardCards;
    public List<GameObject> handCardsObj;

    //CardZone
    public CardZoneDisplay drawCardZoneDisplay;
    public CardZoneDisplay discardZoneDisplay;

    //判斷
    private bool canDraw;

    private void Start()
    {
        canDraw = true;
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
        battleDeck = (List<CardData>)data.mainWeaponDeck.Concat(data.supWeaponDeck);
    }

    public void SaveData(ref GameData data)
    {
        //戰鬥過程中應該無法存擋
    }

    public void ShowCardZoneCOunt()
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
        if (!canDraw)
        {
            Debug.Log("無法抽牌");
            return;
        }
        if (num > battleDeck.Count)
        {
            Debug.Log("卡組數量不夠抽牌，重新把棄牌堆放回");

        }
        GameObject new_card;
        for (int i = 0; i < num; i++)
        {
            CardData cardData = battleDeck[0];//永遠取第一張，然後將其刪掉

            new_card = Instantiate(CardPrefab, HandCardPanel, false);
            new_card.GetComponent<CardDisplay>().CardData = cardData;
            handCardsObj.Add(new_card);

            battleDeck.RemoveAt(0);
            
        }

    }

}
