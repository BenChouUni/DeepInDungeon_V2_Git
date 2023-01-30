using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{
    public CardDataBase CardList;
    public GameObject CardPrefab;
    public Transform CardShowListPanel;
    public RectTransform Panel;

    void Start()
    {
        foreach (CardSO item in CardList.cardList)
        {
            CreateCardOnPanel(item.cardData.id);
        }
    }
    public void CreateCardOnPanel(int id)
    {
        CardData cardData = GetCardData(id);
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

}
