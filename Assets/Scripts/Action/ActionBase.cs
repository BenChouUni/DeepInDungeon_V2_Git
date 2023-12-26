using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase
{
    public abstract ActionType type { get; }
    //CardActionParameter cardActionParameter;


    protected ActionBase() { }

    public abstract void DoAction(ActionParameter parameter);

    /// <summary>
    /// 返回動作描述
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public abstract string ActionDescribe(ActionParameter parameter);
}
