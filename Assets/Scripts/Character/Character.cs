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
    public CharacterType characterType;

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
    public StateEffect stateEffectTest;
    //bool狀態相關
    public bool isDeath;

    //顯示相關委託 更新角色顯示
    public Action<Character> updateDisplay;
    public Action<HpState> hpDisplay;
    public Action<List<StateEffect>> statesDisplay;
    public Action deathBehavior;
    public Action<int> ShowHitNumber;

    //state list
    /// <summary>
    /// 層數為零不會刪除，不要顯示
    /// </summary>
    public Dictionary<StateEffectType, StateEffect> stateDic = new Dictionary<StateEffectType, StateEffect>();
    [SerializeField]
    public List<StateEffect> StateList = new List<StateEffect>();
    //
    public Character(string _name,int _maxHp,int _shield)
    {
        isDeath = false;
        this.characterName = _name;
        this.hpState = new HpState(_maxHp);
        
        this.shield = _shield;
        //Debug.Log("初始化");
        stateDic = new Dictionary<StateEffectType, StateEffect>();
        StateList = new List<StateEffect>();

    }

    /// <summary>
    /// 設定委派函數
    /// </summary>
    /// <param name="_displayAction"></param>
    /// <param name="_hpDisplay"></param>
    public void setDisplayAction(Action<Character> _displayAction, Action<HpState> _hpDisplay,Action<List<StateEffect>> statesDisplayAction,Action deathbeh,Action<int> showHit)
    {
        this.updateDisplay += _displayAction;
        this.hpDisplay += _hpDisplay;
        this.statesDisplay += statesDisplayAction;
        this.deathBehavior += deathbeh;
        this.ShowHitNumber += showHit;
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

        int trueDmg = ShieldBlock(dmg);
        hpState.LostHp(trueDmg);
        ShowHitNumber?.Invoke(trueDmg);
        
        if (hpState.CurrentHp==0)
        {
            Debug.LogFormat("{0}死亡", this.characterName);
            isDeath = true;
            deathBehavior?.Invoke();
        }
        hpDisplay?.Invoke(this.HpState);
        if (hpDisplay==null)
        {
            Debug.Log("hp 顯示器不見");
        }
        updateDisplay?.Invoke(this);
    }
    public void GetDamage(int dmg, bool canBlock)
    {
        if (canBlock)
        {
            GetDamage(dmg);
        }
        else
        {
            if (dmg <= 0 || isDeath)
            {
                return;
            }

            int trueDmg = dmg;
            hpState.LostHp(trueDmg);
            ShowHitNumber?.Invoke(trueDmg);

            if (hpState.CurrentHp == 0)
            {
                Debug.LogFormat("{0}死亡", this.characterName);
                isDeath = true;
                deathBehavior?.Invoke();
            }
            hpDisplay?.Invoke(this.HpState);
            if (hpDisplay == null)
            {
                Debug.Log("hp 顯示器不見");
            }
            updateDisplay?.Invoke(this);
        }
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
        Debug.LogFormat("Add {0}", stateEffect.effectName);
        StateEffectType type = stateEffect.effectType;

        stateEffect.SetRemoveAction(RemoveState);
        if (stateDic == null)
        {
            Debug.LogError("dictionary is null");
            stateDic = new Dictionary<StateEffectType, StateEffect>();
        }
        //如果已經有同樣的狀態了
        if (stateDic.ContainsKey(type))
        {
            if (stateDic[type].Layer == 0)
            {
                Debug.LogFormat("state list Add {0}", stateEffect.effectName);
                StateList.Add(stateDic[type]);
            }
            int addLayer = stateEffect.Layer;
            stateDic[type].AddLayer(addLayer);

            
            //else
            //{
            //    /*
            //    foreach (StateEffect item in StateList)
            //    {
            //        if (item.effectType == type)
            //        {
            //            item.AddLayer(addLayer);
            //        }
            //    }
            //    */
            //}
            
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

    public void DtecAllState()
    {
        for (int i  = StateList.Count - 1; i >= 0; i--)
        {
            StateList[i].DtecLayer();
        }
        //for (StateEffect item in StateList)
        //{
        //    item.DtecLayer();
        //}
    }
    /// <summary>
    /// 當State層數削減，呼叫
    /// </summary>
    public void UpdateStateDisplay()
    {
        statesDisplay?.Invoke(this.StateList);
    }
}
