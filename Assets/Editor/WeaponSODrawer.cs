using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEditor.Callbacks;

//雙點名字開啟視窗
/*
public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        WeaponSO SO = EditorUtility.InstanceIDToObject(instanceId) as WeaponSO;
        if (SO != null)
        {
            WeaponDataEditorWindow.Open(SO);
            return true;
        }
        return false;
    }
}
*/
[CustomEditor(typeof(WeaponSO))]
public class WeaponSODrawer : Editor
{
    
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        WeaponSO weaponSO = (WeaponSO)target;
        WeaponData weaponData = weaponSO.weaponData;

        DrawData(weaponData);
    }

    private static void DrawData(WeaponData weaponData)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Detail");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("ID", GUILayout.MaxWidth(20));
        weaponData.id = EditorGUILayout.IntField(weaponData.id);

        EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(50));
        weaponData.weaponName = EditorGUILayout.TextField(weaponData.weaponName);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("ATK", GUILayout.MaxWidth(25));
        weaponData.atk = EditorGUILayout.IntField(weaponData.atk);

        EditorGUILayout.LabelField("DEF", GUILayout.MaxWidth(25));
        weaponData.def = EditorGUILayout.IntField(weaponData.def);

        EditorGUILayout.LabelField("MaxDistance", GUILayout.MaxWidth(80));
        weaponData.distance = EditorGUILayout.IntField(weaponData.distance);

        EditorGUILayout.EndHorizontal();
    }
}
