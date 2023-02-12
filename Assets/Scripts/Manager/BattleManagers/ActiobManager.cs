using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiobManager : MonoBehaviour
{
    public void UseAction(Action action,Character character)
    {
        int id = action.id;
        int parameter = action.parameter;
        switch (id)
        {
            case 0:
                AttackAction(character, parameter);
                break;
            default:
                break;
        }
    }

    private void AttackAction(Character character,int damage)
    {
        character.GetDamage(damage);
       
    }
}
