using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public EnemyData enemyData;
    //UIShow
    public Text enemyName;
    public HealthBar enemyHealthBar;
    public Text enemyShield;
    public GameObject Shieldinformation;

    private void Awake()
    {
        //暫時使用
        enemyData = new EnemyData(0, "木樁", 50, 0, 3);
        
    }

    private void Start()
    {
        ShowEnemy();
    }

    
    void Update()
    {
        if (enemyData.Shield <= 0)
        {
            Shieldinformation.SetActive(false);
        }
        else
        {
            Shieldinformation.SetActive(true);
        }
    }
    private void ShowEnemy()
    {
        enemyName.text = enemyData.Name;
        enemyHealthBar.SetMaxHealth(enemyData.MaxHp);
        enemyHealthBar.SetHealth(enemyData.CurrentHp);
        enemyShield.text = (enemyData.Shield).ToString();
    }
    public void UpdateEnemyStatus()
    {
        ShowEnemy();
    }


}
