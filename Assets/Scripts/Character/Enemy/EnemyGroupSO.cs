using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyGroup", menuName = "Create SO/Enemy Group SO")]
public class EnemyGroupSO : ScriptableObject
{
    public int id;
    public string groupName;
    public List<EnemySO> enemies;
}
