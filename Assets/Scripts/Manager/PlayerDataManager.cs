using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 在武器庫管理角色資料更動
/// </summary>
public class PlayerDataManager : MonoSingleton<PlayerDataManager>, IDataPersistence
{

    public PlayerData playerData;
    //UI
    public Text playerName_text;
    public Text playerHealth_text;

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">0 is mainWeapon,1 is supportWeapon</param>
    public void SetWeapon(int type,WeaponData weaponData)
    {
        if (type == 0)
        {
            playerData.MainWeaponData = weaponData;
        }
        else if (type == 1)
        {
            playerData.SupportWeaponData = weaponData;
        }
    }

    public void ShowPlayerData()
    {
        if (playerName_text == null)
        {
            Debug.LogError("Cannot find the player name text");
            return;
        }
        else if (playerHealth_text == null)
        {
            Debug.LogError("Cannot find the player health text");
            return;
        }

        playerName_text.text = playerData.Name;
        playerHealth_text.text = playerData.CurrentHp.ToString();
    }
}
