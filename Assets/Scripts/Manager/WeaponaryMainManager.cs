using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponaryMainManager : MonoBehaviour
{
    public WeaponDropZone mainWeaponDropZone;
    public WeaponDropZone supportWeaponDropZone;

    private void Awake()
    {
        mainWeaponDropZone.SetZoneType(0);
        supportWeaponDropZone.SetZoneType(1);
    }
    void Start()
    {
        PlayerDataManager.instance.ShowPlayerData();
        Debug.Log("ShowPlayerData");
    }


}
