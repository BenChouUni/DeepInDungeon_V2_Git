using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour, IDataPersistence
{
    public List<CardData> mainDeck;
    public List<CardData> supDeck;
    public CardDataBase CardList;
    public GameObject CardPrefab;
    public Transform CardShowListPanel;
    //public RectTransform Panel;
    public GameObject DeckDisplay;

    public void LoadData(GameData data)
    {
        this.mainDeck = data.mainWeaponDeck;
        this.supDeck = data.supWeaponDeck;

    }
    public void SaveData(ref GameData data)
    {

    }

    void Start()
    {
        foreach (CardData item in mainDeck)
        {
            CreateCardOnPanel(item);
        }

        foreach (CardData item in supDeck)
        {
            CreateCardOnPanel(item);
        }
    }
    public void CreateCardOnPanel(CardData cardData)
    {
        GameObject new_card;

        if (cardData != null)
        {
            new_card = Instantiate(CardPrefab, CardShowListPanel, false);
            new_card.GetComponent<CardDisplay>().CardData = cardData;
        }
    }
    public CardData GetCardData(int id)
    {
        foreach (CardSO item in CardList.cardList)
        {
            if (item.cardData.id == id)
            {
                return item.cardData;
            }
        }
        return null;
    }

    public void ShowDeck()
    {
        DeckDisplay.SetActive(true);
    }

    public void CloseDeck()
    {
        DeckDisplay.SetActive(false);
    }
}
