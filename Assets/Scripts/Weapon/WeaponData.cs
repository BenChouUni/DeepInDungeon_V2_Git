using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public int id;
    public string weaponName;
    public int atk;
    public int def;
    public int distance;
    
    public WeaponData()
    {
        weaponName = "";
        distance = 0;
        
    }
}
