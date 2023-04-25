using System;
using UnityEngine;


/// <summary>
/// 用來繼承給予其他Action子類
/// </summary>
[Serializable]
public abstract class CardActionBase
{
    public abstract ActionType type { get; }
    //CardActionParameter cardActionParameter;


    protected CardActionBase() {  }

    public abstract void DoAction(CardActionParameter parameter);

    /// <summary>
    /// 返回動作描述
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public abstract string ActionDescribe(CardActionParameter parameter);


}
