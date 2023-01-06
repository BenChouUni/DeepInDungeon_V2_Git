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
        playerData = data.playerData;
    }

    public void SaveData(ref GameData data)
    {
        data.playerData = playerData;
    }
    /// <summary>
    /// 給drop相關使用
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
        if (playerName_text != null)
        {
            playerName_text.text = playerData.Name;
        }

        if (playerHealth_text != null)
        {
            playerHealth_text.text = playerData.CurrentHp.ToString();
        }
 
    }
}
