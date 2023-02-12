using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public EnemyData enemyData;
    //UIShow
    public Text enemyName;
    public HealthBar enemyHealthBar;

    private void Awake()
    {
        //暫時使用
        enemyData = new EnemyData(0, "木樁", 50, 0, 3);
    }
    public void ShowEnemy()
    {
        enemyName.text = enemyData.Name;
        enemyHealthBar.SetMaxHealth(enemyData.MaxHp);
        enemyHealthBar.SetHealth(enemyData.CurrentHp);
    }

}
