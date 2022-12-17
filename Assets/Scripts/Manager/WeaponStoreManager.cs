using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStoreManager : MonoBehaviour
{
    public WeaponDataBase weaponDataBase;

    public GameObject WeaponPrefab;
    public Transform WeaponListPanel;

    private void Start()
    {
        CreateWeaponOnPanel(1);
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
        foreach (WeaponSO item in weaponDataBase.weaponData)
        {
            if (item.weaponData.id == id)
            {
                return item.weaponData;
            }
        }
        return null;
    }
}
