using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// enemy顯示與管理
/// </summary>
public class EnemyManager : MonoSingleton<EnemyManager>,IDataPersistence
{
    [Header("暫時放上敵人SO來試試看效果")]
    public EnemySO enemySO;

   
    [SerializeField]
    public EnemyData enemyData;
    public EffectListDisplay effectListDisplay;
    //UIShow
    public Text enemyName;
    public HealthBar enemyHealthBar;
    public Text enemyShield;
    public GameObject Shieldinformation;

    private void Awake()
    {
        //暫時使用
        //enemyData = new EnemyData(0, "木樁", 50, 0, 3);
        //enemyData = JClone.DeepClone<EnemyData>(enemySO.enemyData);
        //enemyData.hpDisplay += enemyHealthBar.Show;
        
    }

    private void Start()
    {
        enemyData.setDisplayAction(ShowEnemy, enemyHealthBar.Show, effectListDisplay.ShowStateList, EnemyDie,ShowHitNumber);
        ShowEnemy(this.enemyData);
        enemyHealthBar.Show(this.enemyData.HpState);
    }

    
    private void ShowEnemy(Character character)
    {
        //Debug.Log("Show Enemy");
        enemyName.text = character.CharacterName;
        enemyShield.text = (character.Shield).ToString();
        if (character.Shield <= 0)
        {
            Shieldinformation.SetActive(false);
        }
        else
        {
            Shieldinformation.SetActive(true);
        }
    }

    public void DoEnemyAction()
    {
        enemyData.DoAction();
    }

    private void EnemyDie()
    {
        BattleMainManager.instance.WinBattle();
    }

    private void ShowHitNumber(int num)
    {
        BattleMainManager.instance.GenerateHitNum(num, enemyHealthBar.transform);
    }

    public void LoadData(GameData data)
    {
        enemyData = JClone.DeepClone<EnemyData>(data.mapData.Currentlevel.enemy.enemyData);
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
