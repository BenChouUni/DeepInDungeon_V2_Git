using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCardManager : MonoBehaviour
{
    public List<CardSO> InicialCardList;
    public GameObject InicialCardPrefab;
    public Transform InicialCardListPanel;


    private void Start()
    {
        CreateInicialCardOnPanel(1);
    }
    public void CreateInicialCardOnPanel(int id)
    {
        CardData inicialcardData = GetInicialCardData(id);
        GameObject new_inicialcard;

        if (inicialcardData != null)
        {
            new_inicialcard = Instantiate(InicialCardPrefab, InicialCardListPanel, false);
            new_inicialcard.GetComponent<CardDisplay>().CardData = inicialcardData;
        }
    }

    public CardData GetInicialCardData(int id)
    {
        foreach (CardSO item in InicialCardList)
        {
            if (item.cardData.id == id)
            {
                return item.cardData;
            }
        }
        return null;
    }


}
