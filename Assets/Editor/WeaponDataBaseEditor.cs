using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using System.IO;
//using UnityEditor.VersionControl;

[CustomEditor(typeof(WeaponDataBase))]
public class WeaponDataBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WeaponDataBase weaponDataBase = (WeaponDataBase)target;
        List<WeaponData> list = new List<WeaponData>();

        foreach (WeaponSO item in weaponDataBase.weaponDataList)
        {
            
            if (item == null)
            {
                continue;
            }
            list.Add(item.weaponData);
        }

        

        for (int i = 0; i < list.Count; i++)
        {
            CreateWeaponDataRow(list, i);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add New WeaponSO"))
        {
            WeaponSO so = CreateWeaponSOAsset();
            weaponDataBase.weaponDataList.Add(so);
            Repaint();
        }

        if (GUILayout.Button("Save Name"))
        {
            foreach (WeaponSO item in weaponDataBase.weaponDataList)
            {
                SaveName(item);
                EditorUtility.SetDirty(item);
                
            }
            AssetDatabase.SaveAssets();

        }
        if (GUILayout.Button("Save"))
        {
            foreach (WeaponSO item in weaponDataBase.weaponDataList)
            {
                EditorUtility.SetDirty(item);
                
            }
            AssetDatabase.SaveAssets();
        }
    }

    private static void SaveName(WeaponSO item)
    {
        string assetPath = AssetDatabase.GetAssetPath(item.GetInstanceID());
        WeaponData weaponData = item.weaponData;
        Debug.Log(weaponData.weaponName);
        WeaponData NewData = new WeaponData(weaponData.id, weaponData.weaponName, weaponData.atk, weaponData.def, weaponData.distance);

        Debug.Log(NewData.weaponName);
        AssetDatabase.RenameAsset(assetPath, item.weaponData.weaponName);
        Debug.Log(NewData.weaponName);
        //會創建一個新的檔案，舊的會找不到，所以要重新給值



        assetPath = AssetDatabase.GetAssetPath(item.GetInstanceID());
        WeaponSO newSO = AssetDatabase.LoadAssetAtPath<WeaponSO>(assetPath);
        newSO.weaponData = NewData;

        
    }

    private static void CreateWeaponDataRow(List<WeaponData> list, int i)
    {
        if (list[i] == null)
        {
            return;
        }
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("ID", GUILayout.MaxWidth(20));
        list[i].id = EditorGUILayout.IntField(list[i].id);

        EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(45));
        list[i].weaponName = EditorGUILayout.TextField(list[i].weaponName);

        EditorGUILayout.LabelField("ATK", GUILayout.MaxWidth(30));
        list[i].atk = EditorGUILayout.IntField(list[i].atk);

        EditorGUILayout.LabelField("DEF", GUILayout.MaxWidth(30));
        list[i].def = EditorGUILayout.IntField(list[i].def);

        EditorGUILayout.LabelField("Max Distance", GUILayout.MaxWidth(70));
        list[i].distance = EditorGUILayout.IntField(list[i].distance);

        EditorGUILayout.EndHorizontal();
    }



    [MenuItem("Asset/Create/WeaponSO")]
    public static WeaponSO CreateWeaponSOAsset()
    {
        WeaponSO asset = ScriptableObject.CreateInstance<WeaponSO>();
        /*
        if (!asset)
        {
            Debug.LogWarning("WeaponSO not found");
            return;
        }*/

        string path = "Assets/Resources/SO/WeaponSO/NewWeaponSO.asset";

        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        //Selection.activeObject = asset;
        return asset;
    }

}
