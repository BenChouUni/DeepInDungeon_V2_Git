using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerDataManager : MonoSingleton<BattlePlayerDataManager>,IDataPersistence
{
    public PlayerData playerData;

    public Text playerName;
    public HealthBar playerHealthBar;



    public void LoadData(GameData data)
    {
        this.playerData = data.playerData;
    }

    public void SaveData(ref GameData data)
    {
        
    }

    public void ShowPlayerStatus()
    {
        playerName.text = playerData.Name;
        playerHealthBar.SetMaxHealth(playerData.MaxHp);
        playerHealthBar.SetHealth(playerData.CurrentHp);
    }
}
