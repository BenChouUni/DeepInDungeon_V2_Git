using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerDataManager : MonoSingleton<BattlePlayerDataManager>,IDataPersistence
{
    //存讀data
    private PlayerData playerData = new PlayerData();
    //戰鬥中data
    public PlayerData battleplayerData = new PlayerData();


    //生成角色資訊
    public Transform Status;
    public GameObject SmallWeaponPrefab;
    public Transform Canvas;
    public GameObject WeaponInformationPrefab;

    private int currentEnergy;
    public int CurrentEnergy
    {
        get { return currentEnergy;}
    }
    public int maxenergy;

    public Text playerName;
    public HealthBar playerHealthBar;
    public Text playerEnergy;
    public Text playerShield;

    /// <summary>
    /// 在開始時顯示主副手武器
    /// </summary>
    void Start()
    {
        
        ShowWeaponInformation();
        battleplayerData = (PlayerData)this.playerData.Clone();
    }

    public void LoadData(GameData data)
    {
        this.playerData = data.playerData;
        
    }
    public void SaveData(ref GameData data)
    {
        
    }
    public void InitialPlayerStatus()
    {
        this.maxenergy = this.currentEnergy = playerData.Energy;
        ShowPlayerStatus(playerData);
        ShowEnergy();
    }
    private void ShowPlayerStatus(PlayerData _playerData)
    {
        playerName.text = _playerData.Name;
        playerHealthBar.SetMaxHealth(_playerData.MaxHp);
        playerHealthBar.SetHealth(_playerData.CurrentHp);
        
    }
    public void UpdatePlayerStatus()
    {
        ShowPlayerStatus(this.battleplayerData);
        ShowEnergy();
        ShowShield();
    }

    /// <summary>
    /// 顯示主副手武器
    /// </summary>
    public void ShowWeaponInformation()
    {
        /*
        Debug.Log(playerData.MainWeaponData.id);
        Debug.Log(playerData.SupportWeaponData.id);
        */
        GameObject SmallMainWeapon;
        SmallMainWeapon = Instantiate(SmallWeaponPrefab, Canvas, false);
        SmallMainWeapon.GetComponent<WeaponDisplay>().WeaponData = playerData.MainWeaponData;
        //SmallMainWeapon.transform.position = new Vector3(PlayerStatus.position.x, PlayerStatus.position.y, 0);
        SmallMainWeapon.transform.position = new Vector3(Canvas.position.x - 620, Canvas.position.y + 330, 0);
        //Debug.Log(SmallMainWeapon.GetComponent<WeaponDisplay>().WeaponData.id);

        GameObject SmallSupportWeapon;
        SmallSupportWeapon = Instantiate(SmallWeaponPrefab, Canvas, false);
        SmallSupportWeapon.GetComponent<WeaponDisplay>().WeaponData = playerData.SupportWeaponData;
        SmallSupportWeapon.transform.position = new Vector3(Canvas.position.x - 850, Canvas.position.y + 330, 0);
    }
    public GameObject CallWeaponInformation(WeaponData data) 
    {
        
        GameObject WeaponInformation;
        WeaponInformation = Instantiate(WeaponInformationPrefab, Status, false);
        WeaponInformation.GetComponent<WeaponDisplay>().WeaponData = data;
        WeaponInformation.transform.position = new Vector3(Canvas.position.x, Canvas.position.y, 0);
        return WeaponInformation;
    }
    public void RemoveWeaponInformation(GameObject weaponInformation)
    {
        Destroy(weaponInformation);
    }


    //Energy相關
    #region
    private void ShowEnergy()
    {
        playerEnergy.text = string.Format("{0}/{1}", currentEnergy, maxenergy);
    }
    public void ResetEnergy()
    {
        currentEnergy = maxenergy;
        ShowEnergy();
    }
    public void ConsumeEnergy(int num)
    {
        if (num < 0)
        {
            Debug.Log("消耗能量為負");
            return;
        }
        currentEnergy -= num;
        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
        ShowEnergy();
    }
    public void AddEnergy(int num)
    {
        if (num < 0)
        {
            Debug.Log("增加能量為負");
            return;
        }
        currentEnergy += num;
        if (currentEnergy > maxenergy)
        {
            currentEnergy = maxenergy;
        }
        ShowEnergy();
    }
    #endregion

    //Shield相關
    private void ShowShield()
    {
        playerShield.text = (battleplayerData.Shield).ToString();
    }

}



    