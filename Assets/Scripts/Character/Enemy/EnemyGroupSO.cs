using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyGroup", menuName = "Create SO/Enemy Group SO")]
public class EnemyGroupSO : ScriptableObject
{
    public int id;
    public string groupName;
    public EnemyChallengeType challengeType;
    private EnemyGroupData enemygroupdata;
    public EnemyGroupData Enemygroupdata{
        get { 
            if (enemygroupdata == null){
                enemygroupdata = getEnemyGroupdata();  
            }
            return enemygroupdata;
        }
    }
    public List<EnemySO> enemies; 

    private EnemyGroupData getEnemyGroupdata()
    {
        List<EnemyData> _enemies = new List<EnemyData>();
        foreach(EnemySO item in enemies)
        {
            _enemies.Add(item.enemyData);
        }
        return new EnemyGroupData(id, groupName, challengeType, _enemies);
    }
}
