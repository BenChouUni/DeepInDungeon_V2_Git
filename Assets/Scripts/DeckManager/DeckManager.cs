using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DeckManager : MonoSingleton<DeckManager>
{
    [Header("卡牌庫")]
    public CardDataBase cardDataBase;

    //public List<CardSO> InicialCardList;
    public GameObject deckCardPrefab;
    public Transform InicialCardListPanel;

    [SerializeField]
    private List<GameObject> mainWeaponCards;
    private List<GameObject> supWeaponCards;

    public CardData GetCardDataByID(int cardID)
    {
        foreach (CardSO data in cardDataBase.cardList)
        {
            CardData cardData = data.cardData;
            if (cardData.id == cardID)
            {
                return cardData;
            }
        }
        return null;
    }

    public void CreateCardOnPanel(CardData cardData,DropZoneType type)
    {
   
        GameObject new_inicialcard;


        new_inicialcard = Instantiate(deckCardPrefab, InicialCardListPanel, false);
        new_inicialcard.GetComponent<CardDisplay>().CardData = cardData;
        
        if (type == DropZoneType.MainWeapon)
        {
            mainWeaponCards.Add(new_inicialcard);
        }
        else if (type == DropZoneType.SupportWeapon)
        {
            supWeaponCards.Add(new_inicialcard);
        }
    }
    /// <summary>
    /// 給定類型，直接把對應類型刪掉
    /// </summary>
    /// <param name="type"></param>
    public void RemoveCardsByType(DropZoneType type)
    {
        Debug.LogFormat("Remove Cards By {0}",type);
        if (type == DropZoneType.MainWeapon)
        {
            foreach (GameObject item in mainWeaponCards)
            {
                Destroy(item);
            }
            mainWeaponCards.Clear();
        }
        else if (type == DropZoneType.SupportWeapon)
        {
            foreach (GameObject item in supWeaponCards)
            {
                Destroy(item);
            }
            supWeaponCards.Clear();
        }
    }

}
