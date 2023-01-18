
using UnityEngine;
using UnityEditor;

public class WeaponDataEditorWindow : EditorWindow
{
    public static void Open(GameObject gameObject)
    {
        WeaponDataEditorWindow window = GetWindow<WeaponDataEditorWindow>("Weapon Database Window");
    }
}
