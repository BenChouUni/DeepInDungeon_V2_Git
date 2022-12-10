using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStoreManager : MonoBehaviour
{
    public List<WeaponSO> weaponList;

    public GameObject WeaponPrefab;
    public Transform WeaponListPanel;


    public void CreateWeaponOnPanel(int id)
    {
        WeaponData weaponData = GetWeaponData(id);
        if (weaponData != null)
        {
            Instantiate(WeaponPrefab, WeaponListPanel, false);
        }
    }

    public WeaponData GetWeaponData(int id)
    {
        foreach (WeaponSO item in weaponList)
        {
            if (item.weaponData.id == id)
            {
                return item.weaponData;
            }
        }
        return null;
    }
}
