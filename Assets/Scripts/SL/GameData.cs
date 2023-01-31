using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    public List<WeaponData> weaponary;
    public PlayerData playerData;
    public List<CardData> mainWeaponDeck;
    public List<CardData> supWeaponDeck;

    public GameData()
    {
        weaponary = null;
        playerData = new PlayerData();
        mainWeaponDeck = null;
        supWeaponDeck = null;
    }
}
