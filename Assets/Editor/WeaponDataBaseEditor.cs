using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
            list.Add(item.weaponData);
        }

        

        for (int i = 0; i < list.Count; i++)
        {
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
    }
}
