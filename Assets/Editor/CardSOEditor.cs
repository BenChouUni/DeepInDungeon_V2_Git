using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardSOEditor : EditorWindow
{
    private Editor editor;

    [MenuItem("ExtendedEditor/111")]
    public static void ShowObjWindow()
    {
        var window = EditorWindow.GetWindow<CardSOEditor>(true, "Card SO window", true);
        window.editor = Editor.CreateEditor(ScriptableObject.CreateInstance<CardSO>());

    }

    private void OnGUI()
    {
        this.editor.OnInspectorGUI();
    }
}
