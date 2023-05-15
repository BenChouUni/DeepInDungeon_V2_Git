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
    public EffectListDisplay effectListDisplayl;

    //生成角色資訊
    public Transform Status;
    public GameObject SmallWeaponPrefab;
    public Transform Panel;
    public GameObject WeaponInformationPrefab;
    public GameObject Shieldinformation;

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
        this.playerData.setDisplayAction(ShowPlayerCharacter, playerHealthBar.Show,effectListDisplayl.ShowStateList);
        ShowPlayerCharacter(this.playerData);
        playerHealthBar.Show(this.playerData.HpState);

    }
    public void SaveData(ref GameData data)
    {
        
    }
    public void InitialPlayerStatus()
    {
        this.maxenergy = this.currentEnergy = playerData.Energy;
        ShowPlayerCharacter(playerData);
        ShowEnergy();
    }
    private void ShowPlayerCharacter(Character character)
    {
        playerName.text = character.CharacterName;
        if (character.Shield <= 0)
        {
            Shieldinformation.SetActive(false);
        }
        else
        {
            Shieldinformation.SetActive(true);
            ShowShield(character.Shield);
        }

    }
    public void UpdatePlayerStatus()
    {
        //Debug.Log("update");
        ShowPlayerCharacter(this.battleplayerData);
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
        SmallMainWeapon = Instantiate(SmallWeaponPrefab, Panel, false);
        SmallMainWeapon.GetComponent<WeaponDisplay>().WeaponData = playerData.MainWeaponData;
        //SmallMainWeapon.transform.position = new Vector3(PlayerStatus.position.x, PlayerStatus.position.y, 0);
        SmallMainWeapon.transform.position = new Vector3(Panel.position.x - 620, Panel.position.y + 330, 0);
        //Debug.Log(SmallMainWeapon.GetComponent<WeaponDisplay>().WeaponData.id);

        GameObject SmallSupportWeapon;
        SmallSupportWeapon = Instantiate(SmallWeaponPrefab, Panel, false);
        SmallSupportWeapon.GetComponent<WeaponDisplay>().WeaponData = playerData.SupportWeaponData;
        SmallSupportWeapon.transform.position = new Vector3(Panel.position.x - 850, Panel.position.y + 330, 0);
    }
    public GameObject CallWeaponInformation(WeaponData data) 
    {
        
        GameObject WeaponInformation;
        WeaponInformation = Instantiate(WeaponInformationPrefab, Status, false);
        WeaponInformation.GetComponent<WeaponDisplay>().WeaponData = data;
        WeaponInformation.transform.position = new Vector3(Panel.position.x, Panel.position.y, 0);
        return WeaponInformation;
    }
    public void RemoveWeaponInformation(GameObject weaponInformation)
    {
        Destroy(weaponInformation);
    }


    //Energy相關 之後要放到playerData
    #region
    private void ShowEnergy()
    {
        //Debug.Log("energy_update");
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
    private void ShowShield(int shield)
    {
        playerShield.text = shield.ToString();
    }

}



    