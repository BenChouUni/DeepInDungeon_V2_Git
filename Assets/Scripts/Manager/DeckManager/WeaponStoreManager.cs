using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStoreManager : MonoSingleton<WeaponStoreManager>
{
    public WeaponDataBase weaponDataBase;

    [Header("武器模板")]
    public GameObject WeaponPrefab;
    public Transform WeaponListPanel;
    

    public void InitialWeaponStore()
    {
        foreach (WeaponSO item in weaponDataBase.weaponDataList)
        {
            CreateWeaponOnPanel(item.weaponData.id);
        }
            
    }

    public void CreateWeaponOnPanel(int id)
    {
        WeaponData weaponData = GetWeaponData(id);
        GameObject new_weapon;
        if (weaponData != null)
        {
            new_weapon = Instantiate(WeaponPrefab, WeaponListPanel, false);
            new_weapon.GetComponent<WeaponDisplay>().WeaponData = weaponData;
        }
    }

    public WeaponData GetWeaponData(int id)
    {
        foreach (WeaponSO item in weaponDataBase.weaponDataList)
        {
            if (item.weaponData.id == id)
            {
                return item.weaponData;
            }
        }
        return null;
    }
}
