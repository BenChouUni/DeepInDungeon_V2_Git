using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[SerializeField]
public enum LayerConsumeType
{
    AfterUse,TurnEnd,Never
}
[System.Serializable]
public class StateEffect
{
    public string effectName;
    public StateEffectType effectType;
    //0代表 debuff 1代表buff
    public bool isBuff;
    //消耗層數的邏輯
    public LayerConsumeType consumeType;

    //層數，不能被外面修改
    [SerializeField]
    private int layer;
    public int Layer { get { return layer; } }

    //Action
    private Character myCharacter;
    private Action<StateEffect> removeSelf;

    private StateEffect(): this("", true, StateEffectType.NULL,LayerConsumeType.AfterUse)
    {
           
    }
    public StateEffect(string _name,bool _isBuff,StateEffectType _type, LayerConsumeType _coneumType)
    {
        this.effectName = _name;
        this.isBuff = _isBuff;
        this.effectType = _type;
        this.layer = 0;
        this.consumeType = _coneumType;
    }
    /// <summary>
    /// 在state被加入時要呼叫
    /// </summary>
    /// <param name="_myCharacter"></param>
    public void SetRemoveAction(Action<StateEffect> removeAction)
    {
        removeSelf = removeAction;
    }

    public void setLayer(int num) { layer = num; }
    /// <summary>
    /// 可以輸入負數
    /// </summary>
    /// <param name="num"></param>
    public void AddLayer(int num) { layer += num; } //Debug.LogFormat("Add {0}", num); }

    /// <summary>
    /// 每個狀態自己寫什麼時候會消耗掉層數，是預設的功能
    /// </summary>
    public virtual void ConsumeLayer()
    {
        layer -= 1;
        if (layer <= 0)
        {
            layer = 0;
            removeSelf?.Invoke(this);
            //從StateList當中刪除，還會保留在Dictionary
        }
    }
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
    /// 增加攻擊傷害的比率，有傳入參數
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns></returns>
    public virtual int AddExtraDamage() { return 0; }


    /// <summary>
    /// 增加受到收到傷害比率
    /// </summary>
    /// <returns></returns>
    public virtual float AtReceiveDamage() { return 1; }


    #endregion

}
