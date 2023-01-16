using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(WeaponData))]
public class WeaponDataBaseDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty id = property.FindPropertyRelative("id");
        SerializedProperty weaponName = property.FindPropertyRelative("weaponName");
        SerializedProperty atk = property.FindPropertyRelative("atk");
        SerializedProperty def = property.FindPropertyRelative("def");
        SerializedProperty distance = property.FindPropertyRelative("distance");

        Rect labelPosistion = new Rect(position.x, position.y, position.width, position.height);

        int ident = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;




    }
}
