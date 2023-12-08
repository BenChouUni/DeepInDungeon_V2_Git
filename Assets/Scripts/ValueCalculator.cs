using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public static class ValueCalculator
{
    /// <summary>
    /// receiver可以為null
    /// </summary>
    /// <param name="giver"></param>
    /// <param name="receiver"></param>
    /// <param name="basicDmg"></param>
    /// <returns></returns>
    public static int DmgCalculate(Character giver,Character receiver,float basicDmg)
    {
        int result = 0;
        float damagef = basicDmg;
 
        foreach (StateEffect item in giver.StateList)
        {
            damagef += item.AddExtraDamage();
        }
        foreach (StateEffect item in giver.StateList)
        {
            damagef *= item.AtDealDamage();
        }
        if (receiver!=null)
        {
            foreach (StateEffect item in receiver.StateList)
            {
                damagef *= item.AtReceiveDamage();
            }
        }
        

        result = (int)damagef;
 

        return result;
    }

    public static int DefCalculate(Character giver, Character receiver, float basic)
    {
        int result = 0;
        float deff = basic;


        foreach (StateEffect item in receiver.StateList)
        {
            deff += item.AddExtraDef();
        }


        result = (int)deff;
        return result;
    }
}
