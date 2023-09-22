using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

/// <summary>
/// 掛載在卡牌模板上，傳入資料自動顯示卡牌
/// </summary>
public class CardDisplay : MonoBehaviour
{
    //private CardSO cardSO;
    [SerializeField]
    private CardData cardData;
    /// <summary>
    /// 傳入資料後自動顯示
    /// </summary>
    public CardData CardData
    {
        get
        {
            return cardData;
        }
        set
        {
            cardData = value;
            Show();
        }
    }
    //prefab上要有的基本物件
    public Text cardName_text;
    public Text cost_text;
    public Text initialnum_text;
    public Text description_text;
    public Image cardImage;

    //public void SetCardSO(CardSO _cardSO)
    //{
    //    cardSO = _cardSO;
    //    cardData = cardSO.cardData;
    //}

    private void Show()
    {
        cardName_text.text = cardData.cardName;
        cost_text.text = cardData.cost.ToString();
        if (initialnum_text != null)
        {
            initialnum_text.text = cardData.initialnum.ToString();
        }
        if (description_text != null)
        {
            description_text.text = "";
        }

        if (cardImage!=null)
        {
            cardImage.sprite = cardData.cardSprite;
        }
        UpdateDescription();
        
    }

    public void UpdateDescription()
    {
        if (description_text != null)
        {
            description_text.text = getDescription();
        }
    }
    private string getDescription()
    {
        StringBuilder stb = new StringBuilder("");
        foreach (CardActionSet item in cardData.cardActions)
        {
            stb.Append(item.getDescription());
            stb.AppendLine();
        }
        return stb.ToString();
    }
}
