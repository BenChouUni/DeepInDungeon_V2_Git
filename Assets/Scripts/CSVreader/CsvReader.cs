using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CsvReader : MonoBehaviour
{
    public TextAsset textAssetData;

    [System.Serializable]
    public class Weapon
    {
        public int id;
        public string name;
        public int atk;
        public int def;
        public int distance;
        public string hand;
        public string description;
    }

    [System.Serializable]
    public class WeaponList
    {
        public Weapon[] weapon;
    }

    public WeaponList myweaponlist = new WeaponList();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 7 - 1;

        myweaponlist.weapon = new Weapon[tableSize];
        for(int i = 0; i < tableSize; i++)
        {
            myweaponlist.weapon[i] = new Weapon();
            myweaponlist.weapon[i].id = int.Parse(data[7 * (i + 1) + 0]);
            myweaponlist.weapon[i].name = data[7 * (i + 1) + 1];
            myweaponlist.weapon[i].atk = int.Parse(data[7 * (i + 1) + 2]);
            myweaponlist.weapon[i].def = int.Parse(data[7 * (i + 1) + 3]);
            myweaponlist.weapon[i].distance = int.Parse(data[7 * (i + 1) + 4]);
            myweaponlist.weapon[i].hand = data[7 * (i + 1) + 5];
            myweaponlist.weapon[i].description = data[7 * (i + 1) + 6];

        }
    }

}
