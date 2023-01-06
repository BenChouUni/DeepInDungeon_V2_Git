using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 玩家及敵人的基礎
/// </summary>
[Serializable]
public abstract class Character
{
    [SerializeField]
    private string characterName;
    public string Name
    {
        get { return characterName; }
    }
    public bool isDeath;
    //hpstatus
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
    //shield
    [SerializeField]
    private int shield;
    public int Shield
    {
        get { return shield; }
    }
    //state list

    //
    public Character(string _name,int _maxHp,int _shield)
    {
        this.characterName = _name;
        this.maxHp = _maxHp;
        ResetHp();
        this.shield = _shield;
    }

    protected Character()
    {
    }

    public void GetDamage(int dmg)
    {
        if (dmg <= 0 || isDeath)
        {
            return;
        }

        currentHp -= ShieldBlock(dmg);
        if (currentHp<=0)
        {
            isDeath = true;
            currentHp = 0;
        }


    }
    /// <summary>
    /// 輸入傷害 輸出剩下的傷害
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns></returns>
    public int ShieldBlock(int dmg)
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
    }
    /// <summary>
    /// hp設置到最大值
    /// </summary>
    public void ResetHp()
    {
        currentHp = maxHp;
    }
    public void RestoreHealth(int num)
    {
        if (num <= 0)
        {
            return;
        }

        currentHp += num;
        if (currentHp > maxHp)
        {
            ResetHp();
        }
    }


}
