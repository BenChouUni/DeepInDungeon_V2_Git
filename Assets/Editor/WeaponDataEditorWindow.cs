
using UnityEngine;
using UnityEditor;
using static DocumentBuilder.EditorGUITool; //天真的package

public class WeaponDataEditorWindow : EditorWindow
{
    private WeaponSO edittingWeapom;
    private Editor WeaponSOEditor = null;
    private WeaponSO[] weaponSOs;

    [MenuItem("Tools/MyWindows/Weapon Editor")]
    public static void ShowWindow()
    {
        GetWindow<WeaponDataEditorWindow>("Weapon Editor");

    }

    private void OnEnable()
    {
        minSize = new Vector2(500, 500);
        weaponSOs = Resources.LoadAll<WeaponSO>("");
    }

    Vector2 menuScrollViewPos = new Vector2();

    private void OnGUI()
    {
        //分割比例
        float divineWidth = position.width / 4;
        HorizontalGroup(() =>
        {
            //左邊欄位
            menuScrollViewPos = EditorGUILayout.BeginScrollView(menuScrollViewPos, GUILayout.Width(divineWidth));
            VerticalGroup(() =>
            {

                EditorGUILayout.LabelField("Weapon List: ", GUILayout.Width(divineWidth - 20));
                //上方先放一個可以顯示正在編輯物件的物件區
                edittingWeapom = (WeaponSO)EditorGUILayout.ObjectField(edittingWeapom, typeof(CardSO), false, GUILayout.Width(divineWidth - 25));

               

                //Debug.Log("");
                for (int i = 0; i < weaponSOs.Length; i++)
                {
                    //Debug.Log(weaponSOs[i].GetType());
                    //if (weaponSOs[i].GetType() != typeof(WeaponSO)) continue;
                    WeaponSO weaponSO = (WeaponSO)weaponSOs[i];
                    if (GUILayout.Button(weaponSO.name, GUILayout.Width(divineWidth - 30)))
                    {
                        edittingWeapom = weaponSO;
                        //建立Editor物件 
                       WeaponSOEditor = Editor.CreateEditor(edittingWeapom);
                    }
                }
            }, GUILayout.Width(divineWidth - 30));
            EditorGUILayout.EndScrollView();

            divineWidth += 10;
            //右邊欄位
            VerticalGroup(() =>
            {
                EditorGUI.indentLevel = 1;
                EditorGUI.DrawRect(new Rect(divineWidth, 0, position.width - divineWidth, position.height), ColorSet.DarkGray);
                if (WeaponSOEditor != null)
                {
                    //呼叫Editor的OnInspectorGUI
                    WeaponSOEditor.OnInspectorGUI();
                    //標記SO為修改過，之後會自動存擋
                    EditorUtility.SetDirty(edittingWeapom);
                }
            });


            //分隔線作畫 1/4 3/4
            Rect divideLine = new Rect(divineWidth, 0, 2, position.height);
            EditorGUI.DrawRect(divideLine, Color.black);
        });
    }
}
