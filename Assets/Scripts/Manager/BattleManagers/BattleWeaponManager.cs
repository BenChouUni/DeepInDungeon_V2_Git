using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWeaponManager : MonoSingleton<BattleWeaponManager>,IDataPersistence
{

    private WeaponData mainWeapon = new WeaponData();
    private WeaponData suppportweapon = new WeaponData();



    public void LoadData(GameData data)
    {
        mainWeapon = data.playerData.MainWeaponData;
        suppportweapon = data.playerData.SupportWeaponData;
    }

    public void SaveData(ref GameData data)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// 暫時先回傳主武器的攻擊力
    /// </summary>
    /// <returns></returns>
    public int WeaponAttack()
    {
        return mainWeapon.atk;
    }
    /// <summary>
    /// 暫時回傳主武器防禦
    /// </summary>
    /// <returns></returns>
    public int WeaponDefnd()
    {
        return mainWeapon.def;
    }
}
