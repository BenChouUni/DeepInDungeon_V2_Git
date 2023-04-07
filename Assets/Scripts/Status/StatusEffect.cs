using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class StatusEffect:ScriptableObject
{
    public string effectName;
    public EffectEnum effectType;
    //0代表 debuff 1代表buff
    public bool isBuff;

    //層數，不能被外面修改
    private int layer;

    public StatusEffect(): this("", true, EffectEnum.NULL)
    {
           
    }
    public StatusEffect(string _name,bool _isBuff,EffectEnum _type)
    {
        this.effectName = _name;
        this.isBuff = _isBuff;
        this.effectType = _type;
        this.layer = 0;
    }

    public int getLayer() { return layer; }
    public void setLayer(int num) { layer = num; }
    /// <summary>
    /// 可以輸入負數
    /// </summary>
    /// <param name="num"></param>
    public void AddLayer(int num) { layer += num; } //Debug.LogFormat("Add {0}", num); }

    #region
    //多型函數，決定在什麼階段使用
    /// <summary>
    /// 回合開始時發動
    /// </summary>
    public virtual void AtTurnStart() { }
    /// <summary>
    /// 回合結束時發動
    /// </summary>
    public virtual void AtTurnEnd() { }
    /// <summary>
    /// 武器攻擊前時發動
    /// </summary>
    /// <param name="weaponData"></param>
    public virtual void AtWeaponAttackStart(WeaponData weaponData) { }
    /// <summary>
    /// 武器攻擊後發動
    /// </summary>
    /// <param name="weaponData"></param>
    public virtual void AtWeaponAttackEnd(WeaponData weaponData) { }
    /// <summary>
    /// 抽卡前發動
    /// </summary>
    /// <param name="num">抽卡數量</param>
    public virtual void AtDrawCardStart(int num) { }
    /// <summary>
    /// 獲得護盾
    /// </summary>
    /// <param name="shield">護盾值</param>
    public virtual void AtGetShieldStart(int shield) { }
    public virtual void AtGetShieldEnd(int shield) { }

    /// <summary>
    /// 增加攻擊傷害比率
    /// </summary>
    /// <returns></returns>
    public virtual float AtDealDamage() { return 1; }
    /// <summary>
    /// 增加受到收到傷害比率
    /// </summary>
    /// <returns></returns>
    public virtual float AtReceiveDamage() { return 1; }

    
    #endregion

}
