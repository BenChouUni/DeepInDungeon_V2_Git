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

    public WeaponData(int _id,string _name,int _atk,int _def,int _distance)
    {
        this.id = _id;
        this.weaponName = _name;
        this.atk = _atk;
        this.def = _def;
        this.distance = _distance;
    }
}
