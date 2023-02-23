using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCardDatabase", menuName = "Create SO/DataBase/Card")]
public class CardDataBase : ScriptableObject
{
    public CardSO[] cardList;

}
