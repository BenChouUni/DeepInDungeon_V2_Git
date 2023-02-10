using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerDataManager : MonoSingleton<BattlePlayerDataManager>,IDataPersistence
{
    //存讀data
    public PlayerData playerData;
    //戰鬥中data
    public PlayerData battleplayerData;

    private int currentEnergy;
    public int CurrentEnergy
    {
        get { return currentEnergy;}
    }
    public int maxenergy;

    public Text playerName;
    public HealthBar playerHealthBar;
    public Text playerEnergy;


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
    public void ShowPlayerStatus(PlayerData _playerData)
    {
        playerName.text = _playerData.Name;
        playerHealthBar.SetMaxHealth(_playerData.MaxHp);
        playerHealthBar.SetHealth(_playerData.CurrentHp);
        
    }


    //Energy相關
    #region
    public void ShowEnergy()
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
}
