using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCardManager : MonoBehaviour
{
    public List<CardSO> InicialCardList;
    public GameObject InicialCardPrefab;
    public Transform InicialCardListPanel;

    private List<GameObject> mainWeaponCards;
    private List<GameObject> supWeaponCards;

    public void CreateCardOnPanel(CardData cardData,DropZoneType type)
    {
   
        GameObject new_inicialcard;


        new_inicialcard = Instantiate(InicialCardPrefab, InicialCardListPanel, false);
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
