using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DeckManager : MonoSingleton<DeckManager>,IDataPersistence
{
    [Header("卡牌庫")]
    public CardDataBase cardDataBase;

    //public List<CardSO> InicialCardList;
    public GameObject deckCardPrefab;
    public Transform InitialCardListPanel;

    [SerializeField]
    private List<GameObject> mainWeaponCards;
    [SerializeField]
    private List<GameObject> supWeaponCards;

    public List<CardData> mainWeaponDeck;
    public List<CardData> supWeaponDeck;


    public CardData GetCardDataByID(int cardID)
    {
        foreach (CardSO data in cardDataBase.cardList)
        {
            CardData cardData = data.cardData;
            if (cardData.id == cardID)
            {
                cardData.setSelf(PlayerDataManager.instance.playerData);
                return cardData;
            }
        }
        return null;
    }

    public void CreateCardOnPanel(CardData cardData,WeaponDropZoneType type)
    {
        Debug.Log(cardData.cardName + " " + cardData.initialnum);
        GameObject new_initialcard;


        new_initialcard = Instantiate(deckCardPrefab, InitialCardListPanel, false);
        new_initialcard.GetComponent<CardDisplay>().CardData = cardData;
        
        if (type == WeaponDropZoneType.MainWeapon)
        {

            
            for(int i = 0; i < cardData.initialnum; i++)
            {
                mainWeaponCards.Add(new_initialcard);
                mainWeaponDeck.Add(cardData);
            }
        }
        else if (type == WeaponDropZoneType.SupportWeapon)
        {
            for (int i = 0; i < cardData.initialnum; i++)
            {
                supWeaponCards.Add(new_initialcard);
                supWeaponDeck.Add(cardData);
            }
        }
    }
    /// <summary>
    /// 給定類型，直接把對應類型刪掉
    /// </summary>
    /// <param name="type"></param>
    public void RemoveCardsByType(WeaponDropZoneType type)
    {
        Debug.LogFormat("Remove Cards in {0}",type);
        if (type == WeaponDropZoneType.MainWeapon)
        {
            foreach (GameObject item in mainWeaponCards)
            {
                Destroy(item);
            }
            mainWeaponCards.Clear();

            mainWeaponDeck.Clear();
        }
        else if (type == WeaponDropZoneType.SupportWeapon)
        {
            foreach (GameObject item in supWeaponCards)
            {
                Destroy(item);
            }
            supWeaponCards.Clear();

            supWeaponDeck.Clear();
        }
    }

    public void LoadData(GameData data)
    {
        //只讀武器資料，武器資料自己會生成
    }

    public void SaveData(ref GameData data)
    {
        data.mainWeaponDeck = this.mainWeaponDeck;
        data.supWeaponDeck = this.supWeaponDeck;
    }

    public bool CheckEmpty(WeaponDropZoneType type)
    {
        if (type == WeaponDropZoneType.MainWeapon)
        {
            return (mainWeaponDeck.Count == 0);
        }
        else if (type == WeaponDropZoneType.SupportWeapon)
        {
            return (supWeaponDeck.Count == 0);
        }
        Debug.LogError("CheckEmpty error");
        return false;
    }
}
