using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WoundEffect",menuName ="Asset/CreateEffect/Wound")]
public class WoundEffect : StatusEffect
{
    //初始化
    public WoundEffect():base("Wound",false,EffectEnum.Wound,LayerConsumeType.TurnEnd)
    {
        
    }

    /// <summary>
    /// 受到傷害時多增加1.5倍
    /// </summary>
    /// <returns></returns>
    public override float AtReceiveDamage()
    {
        Debug.Log("判定到Wound");
        return 1.5f;
    }
}
