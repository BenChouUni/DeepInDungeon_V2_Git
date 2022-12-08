using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    private WeaponData weaponData;
    public WeaponData WeaponData
    {
        get { return weaponData; }
    }

    //UI
    public Text weaponName_text;
    public Text atk_text;
    public Text def_text;
    public Text distance_text;
    public Text discription_text;

    private void Show()
    {
        weaponName_text.text = weaponData.weaponName;
        atk_text.text = weaponData.atk.ToString();
        def_text.text = weaponData.def.ToString();
        distance_text.text = string.Format("最遠距離{0}", weaponData.distance);
        
    }
}
