using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 掛載在武器模板上，傳入資料自動顯示
/// </summary>
public class WeaponDisplay : MonoBehaviour
{
    private WeaponData weaponData;
    /// <summary>
    /// 傳入資料後自動顯示
    /// </summary>
    public WeaponData WeaponData
    {
        get { return weaponData; }
        set
        {
            this.weaponData = value;
            Show();
        }
    }

    //UI
    public Text weaponName_text;
    public Text atk_text;
    public Text def_text;
    public Text distance_text;
    public Text discription_text;

    public Image image;

    private void Show()
    {
        if (weaponName_text != null)
        {
            weaponName_text.text = weaponData.weaponName;
        }
        if (atk_text != null)
        {
            atk_text.text = weaponData.atk.ToString();
        }
        if (def_text != null)
        {
            def_text.text = weaponData.def.ToString();
        }
        if (distance_text != null)
        {
            distance_text.text = weaponData.distance.ToString();
        }
        /*
        if (distance_text != null)
        {
            distance_text.text = string.Format("最遠距離{0}", weaponData.distance);
        }
        */
        if (discription_text != null)
        {
            discription_text.text = weaponData.weaponDescription;
        }
        if (image!=null)
        {
            //image.sprite = weaponData.image;
        }
    }
}
