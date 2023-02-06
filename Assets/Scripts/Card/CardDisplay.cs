using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 掛載在卡牌模板上，傳入資料自動顯示卡牌
/// </summary>
public class CardDisplay : MonoBehaviour
{
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
        set {
            cardData = value;
            Show();
        }
    }
    //prefab上要有的基本物件
    public Text cardName_text;
    public Text cost_text;
    public Text initialnum_text;
    public Text description_text;

    private void Show()
    {
        cardName_text.text = cardData.cardName;
        cost_text.text = cardData.cost.ToString();
        initialnum_text.text = cardData.initialnum.ToString();
        if (description_text != null)
        {
            description_text.text = "";
        }
        
    }

}
