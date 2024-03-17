using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData : Character
{
    
    [SerializeField]
    private int id;
    public int ID
    {
        get { return id; }
    }
    [SerializeField]
    private int atk;
    public int ATK
    {
        get { return atk; }
    }

    [SerializeField]
    private int iniactionIndex = 0;     //一串動作的開始，在初始化附加效果之後
    public int IniactionIndex
    {
        get { return iniactionIndex; }
    }

    int nowactionIndex = 0;//現在動作索引

    //新增敵人標籤
    public MonstersType monstersType; //怪物類型
    public EnemyChallengeType challengeType;
    public EnemyClassType enemyClassType;//敵人階級:普通菁英首領
    //public Sprite image;
    //可能用做AI的實現
    [SerializeField]
    public List<EnemyActionSet> actionList;

    private EnemyActionSet nextEnemyAction;
    public EnemyActionSet NextEnemyAction
    {
        get
        {
            if (nextEnemyAction == null) { nextEnemyAction = actionList[0]; }
            return nextEnemyAction;
        }
    }

    public EnemyData(int _id,string _name, int _maxHp, int _shield,int _atk) : base(_name, _maxHp, _shield)
    {
        this.id = _id;
        this.atk = _atk;
        this.characterType = CharacterType.Enemy;
        nowactionIndex = 0;


    }

    public void SetSelf(Character self)
    {
        foreach (EnemyActionSet item in actionList)
        {
            item.enemyActionParameter.setSelf(self);
        }
    }

    public void SetTarget(Character target)
    {
        foreach (EnemyActionSet item in actionList)
        {
            item.enemyActionParameter.setTarget(target);
        }
    }
    
    public void DoAction()
    {
        Debug.LogFormat("{0}執行動作",this.CharacterName);
        //暫時
        //foreach (var item in actionList)
        //{
        //    item.DoAction(this);
        //}
        //輪流做
        if (actionList.Count == 0)
        {
            Debug.Log("敵人沒有行為");
            return;
        }
        //執行下一步的行為
        NextEnemyAction.DoAction(this);
        nowactionIndex++;
        //如果動作序列輪完後
        if (nowactionIndex >= actionList.Count)
        {
            nowactionIndex = iniactionIndex;
        }
        nextEnemyAction = actionList[nowactionIndex];
    }

    public void IniPrepare()    //初始化附加效果
    {
        if(nowactionIndex < iniactionIndex)
        {
            NextEnemyAction.DoAction(this);
            nowactionIndex++;
            nextEnemyAction = actionList[nowactionIndex];
        }
        return;
    }
}
