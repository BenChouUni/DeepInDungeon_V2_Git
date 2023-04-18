using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 玩家及敵人的基礎 名字 血量 護盾 死亡 狀態列
/// </summary>
[Serializable]
public abstract class Character
{
    [SerializeField]
    public CharaterType targetType;

    [SerializeField]
    private string characterName;
    public string CharacterName
    {
        get { return characterName; }
    }

    //hpstate
    [SerializeField]
    private HpState hpState;
    public HpState HpState
    {
        get { return hpState; }
    }
    //shield
    [SerializeField]
    private int shield;
    public int Shield
    {
        get { return shield; }
    }
    /// <summary>
    /// 角色狀態列，先公開給外面操作
    /// </summary>
    //public List<StatusEffect> effectList;

    //bool狀態相關
    public bool isDeath;

    //顯示相關委託 更新角色顯示
    public Action<Character> updateDisplay;
    public Action<HpState> hpDisplay;
    public Action<List<StateEffect>> statesDisplay;

    //state list
    /// <summary>
    /// 層數為零不會刪除，不要顯示
    /// </summary>
    public readonly Dictionary<StateEffectType, StateEffect> stateDic = new Dictionary<StateEffectType, StateEffect>();
    public readonly List<StateEffect> StateList = new List<StateEffect>();
    //
    public Character(string _name,int _maxHp,int _shield)
    {
        isDeath = false;
        this.characterName = _name;
        this.hpState = new HpState(_maxHp);
        
        this.shield = _shield;
       
       
    }
    /// <summary>
    /// 設定委派函數
    /// </summary>
    /// <param name="_displayAction"></param>
    /// <param name="_hpDisplay"></param>
    public void setDisplayAction(Action<Character> _displayAction, Action<HpState> _hpDisplay,Action<List<StateEffect>> statesDisplayAction)
    {
        this.updateDisplay += _displayAction;
        this.hpDisplay += _hpDisplay;
        this.statesDisplay = statesDisplayAction;
    }
    public void setCharaterName(string name)
    {
        this.characterName = name;
        updateDisplay?.Invoke(this);
    }
    

    public void GetDamage(int dmg)
    {
        if (dmg <= 0 || isDeath)
        {
            return;
        }

        hpState.LostHp(ShieldBlock(dmg));
        
        if (hpState.CurrentHp==0)
        {
            Debug.LogFormat("{0}死亡", this.characterName);
            isDeath = true;
            
        }

        updateDisplay?.Invoke(this);
    }
    /// <summary>
    /// 輸入傷害 輸出剩下的傷害
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns></returns>
    private int ShieldBlock(int dmg)
    {
        if (shield != 0)
        {
            if (dmg >= shield)
            {
                dmg -= shield;
                shield = 0;
            }
            else
            {
                shield -= dmg;
                return 0;
            }
        }
        return dmg;
    }

    public void AddShield(int num)
    {
        if (num <= 0)
        {
            return;
        }

        shield += num;
        updateDisplay?.Invoke(this);
    }

    /*stateEffect*/

    /// <summary>
    /// 加入狀態
    /// </summary>
    /// <param name="stateEffect"></param>
    public void AddStateEffect(StateEffect stateEffect)
    {
        StateEffectType type = stateEffect.effectType;

        stateEffect.SetRemoveAction(RemoveState);

        if (stateDic.ContainsKey(type))
        {
            int addLayer = stateEffect.Layer;
            stateDic[type].AddLayer(addLayer);
            foreach (StateEffect item in StateList)
            {
                if (item.effectType == type)
                {
                    item.AddLayer(addLayer);
                }
            }
        }
        else
        {
            stateDic.Add(type, stateEffect);
            StateList.Add(stateEffect);
        }

        updateDisplay?.Invoke(this);
        statesDisplay?.Invoke(this.StateList);
    }
    /// <summary>
    /// 回傳不為零（可互動的）stateEffect
    /// </summary>
    /// <returns></returns>
    public List<StateEffect> GetStateList()
    {
        List<StateEffect> stateList = new List<StateEffect>();
        foreach (KeyValuePair<StateEffectType,StateEffect> kvp in stateDic)
        {
            //如果層數大於零
            if (kvp.Value.Layer > 0)
            {
                stateList.Add(kvp.Value);
            }
        }
       
        return stateList;
    }
    /// <summary>
    /// 刪除StateEffect 由StateEffect自主刪除
    /// </summary>
    /// <param name="stateEffect"></param>
    public void RemoveState(StateEffect stateEffect)
    {
        StateList.Remove(stateEffect);
        updateDisplay?.Invoke(this);
        statesDisplay?.Invoke(this.StateList);
    }
    
}
