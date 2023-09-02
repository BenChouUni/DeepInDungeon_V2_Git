using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// enemy顯示與管理
/// </summary>
public class EnemyManager : MonoSingleton<EnemyManager>,IDataPersistence
{
    //[Header("暫時放上敵人SO來試試看效果")]
    //public EnemySO enemySO;

    //public EnemyGroupSO enemyGroup;
    public EnemyGroupData enemyGroupData;
    public GameObject EnemyPrefab;
    public Transform EnemyArea;
    private List<EnemyControl> EnemyControls;

    /*
    [SerializeField]
    //public EnemyData enemyData;
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
    public GameObject otherIcon;*/

    private void Awake()
    {
        //暫時使用
        //enemyData = new EnemyData(0, "木樁", 50, 0, 3);
        //enemyData = JClone.DeepClone<EnemyData>(enemySO.enemyData);
        //enemyData.hpDisplay += enemyHealthBar.Show;
        
    }

    private void Start()
    {
        if (enemyGroupData == null)
        {
            Debug.LogError("沒有敵人組合");
        }
        //enemyData?.setDisplayAction(ShowEnemy, enemyHealthBar.Show, effectListDisplay.ShowStateList, EnemyDie,ShowHitNumber);
        //ShowEnemy(this.enemyData);
        //enemyHealthBar.Show(this.enemyData.HpState);
        //HideEnemyAction();
        foreach (EnemyData item in enemyGroupData.enemies)
        {
            GameObject obj = Instantiate(EnemyPrefab, EnemyArea);
            EnemyControl enemyControl = obj.GetComponent<EnemyControl>();
            enemyControl.setEnemyData(item);
            this.EnemyControls.Add(enemyControl);
        }
    }

    void Update()
    {
        //enemyData?.DtecAllState();
    }
    /*
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
    */
    public void DoEnemyAction()
    {
        foreach (EnemyData item in enemyGroupData.enemies)
        {
            item.DoAction();
        }
    }
    /*
    private void EnemyDie()
    {
        BattleMainManager.instance.WinBattle();
    }

    private void ShowHitNumber(int num)
    {
        //BattleMainManager.instance.GenerateHitNum(num, enemyHealthBar.transform);
    }
    */
    public void LoadData(GameData data)
    {
        if(data.mapData.Currentlevel.EnemyGroup != null)
        {
            enemyGroupData = JClone.DeepClone<EnemyGroupData>(data.mapData.Currentlevel.EnemyGroup);
            Debug.Log("mapdata的enemygroup不為空");
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
        foreach (var item in EnemyControls)
        {
            item.ShowEnemyAction();
        }
    }

    public void HideEnemyAction()
    {
        foreach (var item in EnemyControls)
        {
            item.HideEnemyAction();
        }
    }
    private void ShowIcon(GameObject icon,string str)
    {
        icon?.SetActive(true);
        icon.GetComponentInChildren<Text>().text = str;
    }
    
}
