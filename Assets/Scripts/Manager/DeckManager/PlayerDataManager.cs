using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
/// <summary>
/// 在武器庫管理角色資料更動以及顯示
/// </summary>
public class PlayerDataManager : MonoSingleton<PlayerDataManager>, IDataPersistence
{

    public PlayerData playerData;
    //UI
    public Text playerName_text;
    public HealthBar playerHpBar;

    public void LoadData(GameData data)
    {
        playerData = data.playerData;
        this.playerData.setDisplayAction(ShowPlayerDataCharacter, playerHpBar.Show,null,null);
        ShowPlayerDataCharacter(this.playerData);
        playerHpBar.Show(this.playerData.HpState);
    }

    public void SaveData(ref GameData data)
    {
        data.playerData = playerData;
    }

    /// <summary>
    /// 給drop相關使用
    /// </summary>
    /// <param name="type">0 is mainWeapon,1 is supportWeapon</param>
    public void SetWeapon(WeaponDropZoneType type,WeaponData weaponData)
    {

        Debug.LogFormat("{0} is set as {1}", type.ToString(), weaponData.weaponName);
        if (type == WeaponDropZoneType.MainWeapon)
        {
            playerData.MainWeaponData = weaponData;
        }
        else if (type == WeaponDropZoneType.SupportWeapon)
        {
            playerData.SupportWeaponData = weaponData;
        }
    }
    public void RemoveWeapon(WeaponDropZoneType type)
    {
        Debug.LogFormat("{0} is removed ", type.ToString());
        if (type == WeaponDropZoneType.MainWeapon)
        {
            playerData.MainWeaponData = null;
        }
        else if (type == WeaponDropZoneType.SupportWeapon)
        {
            playerData.SupportWeaponData = null;
        }
    }
    public void ShowPlayerDataCharacter(Character character)
    {
        if (playerName_text != null)
        {
            playerName_text.text = character.CharacterName;
        }

    }
}
