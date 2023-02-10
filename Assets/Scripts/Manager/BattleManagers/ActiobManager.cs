using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiobManager : MonoBehaviour
{
    public void UseAction(Action action,Character character)
    {

    }

    private void AttackAction(Character character,int damage)
    {
        character.GetDamage(damage);
       
    }
}
