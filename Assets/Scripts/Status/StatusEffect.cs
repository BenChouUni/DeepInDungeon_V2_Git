using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatusType
{
    None,

}
[System.Serializable]
public class StatusEffect
{
    public StatusType statusType;
    public int layer;
    //0代表 debuff 1代表buff
    public bool isBuff;


    public StatusEffect()
    {
        this.statusType = StatusType.None;
        this.layer = 0;
        this.isBuff = true;
    }
    public StatusEffect(StatusType _type,int _layer,bool _isBuff)
    {
        this.statusType = _type;
        this.layer = _layer;
        this.isBuff = _isBuff;
    }
}
