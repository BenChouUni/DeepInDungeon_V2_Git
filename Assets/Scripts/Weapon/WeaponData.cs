using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class WeaponData
{
    public int id;
    public string weaponName;
    public int atk;
    public int def;
    public int distance;

    //public Sprite image;
    public string weaponDescription;

    public WeaponType weaponType;

    public Sprite weaponSprite;
    
    public WeaponData()
    {
        weaponName = "";
        distance = 0;
        this.weaponType = WeaponType.Both;
        
    }

    public WeaponData(int _id,string _name,int _atk,int _def,int _distance,WeaponType _type)
    {
        this.id = _id;
        this.weaponName = _name;
        this.atk = _atk;
        this.def = _def;
        this.distance = _distance;
        this.weaponType = _type;
    }
}
