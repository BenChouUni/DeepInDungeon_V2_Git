using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroupData 
{
    public int id;
    public string groupName;
    public EnemyChallengeType challengeType;
    public List<EnemyData> enemies = new List<EnemyData>();

    public EnemyGroupData(int _id, string _groupname, EnemyChallengeType _challengeType, List<EnemyData> _enemies)
    {
        this.id = _id;
        this.groupName = _groupname;
        this.challengeType = _challengeType;
        this.enemies = _enemies;
    }
}
