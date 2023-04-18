using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// cardAction套組，包括參數標籤enum以及執行方法
/// </summary>
[System.Serializable]
public class CardActionSet 
{
    public ActionType actionType;

    public CardActionBase CardActionBase
    {
        get {
            if (cardActionBase==null)
            {
                //通過工廠模式去建造
                cardActionBase = CardActionFactory.GetCardAction(actionType);
            }
            return cardActionBase;
        }
    }
    private CardActionBase cardActionBase = null;

    public CardActionParameter actionParameter;

    /// <summary>
    /// 執行效果，參數自己會傳入
    /// </summary>
    public void DoAction()
    {
        CardActionBase.DoAction(actionParameter);
    }
    
}
