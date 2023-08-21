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
    public Image EnemyImage;
    //ActionIcon
    public GameObject attackIcon;
    public GameObject defendIcon;
    public GameObject otherIcon;

    private void Awake()
    {
        //暫時使用
        //enemyData = new EnemyData(0, "木樁", 50, 0, 3);
        //enemyData = JClone.DeepClone<EnemyData>(enemySO.enemyData);
        //enemyData.hpDisplay += enemyHealthBar.Show;
        
    }

    private void Start()
    {
        if (enemyData == null)
        {
            Debug.Log("沒有敵人");
        }
        enemyData?.setDisplayAction(ShowEnemy, enemyHealthBar.Show, effectListDisplay.ShowStateList, EnemyDie,ShowHitNumber);
        ShowEnemy(this.enemyData);
        enemyHealthBar.Show(this.enemyData.HpState);
        HideEnemyAction();
    }

    void Update()
    {
        enemyData?.DtecAllState();
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
        if (EnemyImage!=null)
        {
            //EnemyImage.sprite = enemyData.image;
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
        //BattleMainManager.instance.GenerateHitNum(num, enemyHealthBar.transform);
    }

    public void LoadData(GameData data)
    {
        if(data.mapData.Currentlevel.enemy != null)
        {
            enemyData = JClone.DeepClone<EnemyData>(data.mapData.Currentlevel.enemy.enemyData);
        }
        else
        {
            Debug.Log("currentlevel的enemy為空");
        }
        
    }

    public void SaveData(ref GameData data)
    {
        
    }
    /// <summary>
    /// 顯示敵人的下一步動作
    /// </summary>
    public void ShowEnemyAction()
    {
        Debug.Log("顯示敵人行為：");
        EnemyActionSet nextAction = enemyData.NextEnemyAction;
        EnemyActionParameter nextParameter = nextAction.enemyActionParameter;
        if (nextParameter == null)
        {
            Debug.LogError("next parameter is null");
        }
        switch (nextAction.actionType)
        {
            case EnemyActionType.Attack:
                Debug.Log("敵人下回合要攻擊");
                ShowIcon(attackIcon, ValueCalculator.DmgCalculate(nextParameter, enemyData.ATK).ToString());
                break;
            case EnemyActionType.Defend:
                Debug.Log("敵人要防禦");
                ShowIcon(defendIcon, ValueCalculator.DefCalculate(nextParameter, enemyData.ATK).ToString());
                break;

            default:
                Debug.Log("敵人使用特殊行為");
                ShowIcon(otherIcon, ""); 
                break;
        }
    }

    public void HideEnemyAction()
    {
        attackIcon?.SetActive(false);
        defendIcon?.SetActive(false);
        otherIcon?.SetActive(false);
    }
    private void ShowIcon(GameObject icon,string str)
    {
        icon?.SetActive(true);
        icon.GetComponentInChildren<Text>().text = str;
    }
}
