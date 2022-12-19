using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{
    public List<CardSO> CardList;
    public GameObject CardPrefab;
    public Transform CardShowListPanel;
    public RectTransform Panel;

    void Start()
    {
        int num = 0;
        for(int i = 0; i < 20; i++)
        {
            foreach (CardSO item in CardList)
            {
                num++;

                //if (num > 8 &&�@num % 4 == 1)
                //{
                //    Panel.transform.localScale = Vector3.up += 340;
                //}
                
                CreateCardOnPanel(item.cardData.id);
            }
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
        foreach (CardSO item in CardList)
        {
            if (item.cardData.id == id)
            {
                return item.cardData;
            }
        }
        return null;
    }

}
