using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 血量相關的自我管理類
/// </summary>
[Serializable]
public class HpState 
{
    public Action<HpState> hpDisplay;

    [SerializeField]
    private int maxHp;
    public int MaxHp
    {
        get { return maxHp; }
    }
    [SerializeField]
    private int currentHp;
    public int CurrentHp
    {
        get { return currentHp; }
    }


    //只要一個參數就會自動初始化
    public HpState(int _maxHp)
    {
        this.currentHp = _maxHp;
        this.maxHp = _maxHp;
    }


    public void ResetHp()
    {
        this.currentHp = maxHp;
        hpDisplay?.Invoke(this);
    }
    public void LostHp(int num)
    {
        if (num <= 0) return;
        this.currentHp -= num;
        if (currentHp <= 0)
        {
            currentHp = 0;//血量不低於0
        }
        hpDisplay?.Invoke(this);
    }

    public void LostMaxHp(int num)
    {
        if (num <= 0) return;
        this.maxHp -= maxHp;
        if (maxHp<=0)
        {
            maxHp = 0;
        }
        if (currentHp>=maxHp)
        {
            currentHp = maxHp;
        }
        hpDisplay?.Invoke(this);
    }

    public void RestoreHp(int num)
    {
        if (num <= 0) return;
        this.currentHp += num;
        if (currentHp >= maxHp)
        {
            ResetHp();
        }
        hpDisplay?.Invoke(this);
    }
    /// <summary>
    /// 提升血量上限的同時，會增加給原本hp
    /// </summary>
    /// <param name="num"></param>
    public void GainMaxHp(int num)
    {
        if (num <= 0) return;

        this.currentHp += num;
        this.maxHp += num;
        hpDisplay?.Invoke(this);
    }
    /// <summary>
    /// 設定血量，戰鬥結束後使用
    /// </summary>
    public void setCurrentHp(int _currentHp)
    {
        this.currentHp = _currentHp;
    }
}
