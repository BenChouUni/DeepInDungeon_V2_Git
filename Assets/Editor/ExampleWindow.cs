using UnityEngine;
using UnityEditor;

public class ExampleWindow : EditorWindow
{
    string myString = "Hello World";

    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Example");
    }
    private void OnGUI()
    {
        GUILayout.Label("This is a label", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("my string", myString);

        if (GUILayout.Button("Press me"))
        {
            Debug.Log("Press");
        }
    }
}
