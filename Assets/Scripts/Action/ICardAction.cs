using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardAction
{
    public void Act(CardData card, Character actor,Character target);
    
}
