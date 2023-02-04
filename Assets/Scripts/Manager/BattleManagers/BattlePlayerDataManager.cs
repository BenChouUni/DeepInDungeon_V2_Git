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
        ShowPlayerStatus(playerData);
    }
    public void ShowPlayerStatus(PlayerData _playerData)
    {
        playerName.text = _playerData.Name;
        playerHealthBar.SetMaxHealth(_playerData.MaxHp);
        playerHealthBar.SetHealth(_playerData.CurrentHp);
        playerEnergy.text = _playerData.Energy.ToString();
    }
}
