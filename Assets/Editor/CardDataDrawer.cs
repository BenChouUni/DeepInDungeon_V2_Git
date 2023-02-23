using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(CardData))]
public class CardDataDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect[] pos = new Rect[3];
        pos[0] = position;
        pos[0].height = 18;
        pos[1] = pos[0];
        pos[1].y += 18;
        pos[2] = pos[1];
        pos[2].y += 18;

        //base.OnGUI(position, property, label);
        //SerializedProperty cardData = property.FindPropertyRelative("cardData");
        Debug.Log(property.FindPropertyRelative("cardName").stringValue);
        EditorGUI.PropertyField(pos[0], property);
        //EditorGUI.LabelField(pos[1], "ttest");

    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //return base.GetPropertyHeight(property, label);
        return 18;
    }
}
