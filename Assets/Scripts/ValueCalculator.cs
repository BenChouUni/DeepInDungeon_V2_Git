using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public static class ValueCalculator
{
    public static int DmgCalculate(ActionParameter parameter,int atk)
    {
        int result = 0;
        float damagef = (parameter.value + atk);

        Character self = parameter.Self;
        Character target = parameter.Target;
        foreach (StateEffect item in self.StateList)
        {
            damagef += item.AddExtraDamage();
        }
        foreach (StateEffect item in self.StateList)
        {
            damagef *= item.AtDealDamage();
        }
        foreach (StateEffect item in target.StateList)
        {
            damagef *= item.AtReceiveDamage();
        }

        result = (int)damagef;
 

        return result;
    }

    public static int DefCalculate(ActionParameter parameter, int def)
    {
        int result = 0;
        float deff = (parameter.value + def);
        Character self = parameter.Self;
        Character target = parameter.Target;

        foreach (StateEffect item in self.StateList)
        {
            deff += item.AddExtraDef();
        }


        result = (int)deff;
        return result;
    }
}
