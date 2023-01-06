using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataBase",menuName = "Create SO/DataBase/Weapon")]
public class WeaponDataBase : ScriptableObject
{
    public List<WeaponSO> weaponData;

}
