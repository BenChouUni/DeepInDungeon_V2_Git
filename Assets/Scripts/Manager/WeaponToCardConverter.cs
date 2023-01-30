using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponToCardConverter : MonoSingleton<WeaponToCardConverter>
{
    public TextAsset weaponToCardTable;

    /// <summary>
    /// 武器ID對應卡牌ID
    /// </summary>
    /// <param name="weaponID"></param>
    /// <returns></returns>
    public List<int> WeaponIdToCardId(int weaponID)
    {
        List<int> result = new List<int>();
        string[] rows = weaponToCardTable.text.Split('\n');
        foreach (string row in rows)
        {
            string[] col = row.Split(',');
            if (int.Parse(col[0]) == weaponID)
            {
                for (int i = 2; i < col.Length; i++)
                {
                    if (col[i]!=null)
                    {
                        result.Add(int.Parse(col[i]));
                        Debug.Log(col[0] + ":" + col[i]);
                    }
                }
            }

        }

        return result;
    } 

}
