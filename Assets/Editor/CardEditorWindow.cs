using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static DocumentBuilder.EditorGUITool; //天真的package

public class CardEditorWindow : EditorWindow
{
    DefaultAsset CardSoFolder = null;
    private CardSO edittingCardSO;
    private Editor CardSOEditor = null;
    [MenuItem("Tools/MyWindows/Card Editor")]
    public static void ShowWindow()
    {
        GetWindow<CardEditorWindow>("Card Editor");

    }

    CardSO[] cardSOs;
    private void OnEnable()
    {
        //最小視窗大小
        minSize = new Vector2(500, 500);
        cardSOs = Resources.LoadAll<CardSO>("");

    }
    //儲存左邊menu欄位瀏覽中的座標位置
    Vector2 menuScrollViewPos = new Vector2();

    public void OnGUI()
    {
        //分割比例
        float divineWidth = position.width / 4;
        HorizontalGroup(() =>
        {
            //左邊欄位
            VerticalGroup(() =>
            {
                EditorGUILayout.LabelField("Card List: ");
               
                edittingCardSO = (CardSO)EditorGUILayout.ObjectField(edittingCardSO, typeof(CardSO), false);
               
                menuScrollViewPos = EditorGUILayout.BeginScrollView(menuScrollViewPos,GUILayout.Width(divineWidth-10));
                //Folder放置
                //EditorGUILayout.LabelField("Card So Folder");
                //CardSoFolder = (DefaultAsset)EditorGUILayout.ObjectField( CardSoFolder, typeof(DefaultAsset), false,GUILayout.Width(divineWidth-5));

                //調用對應資料夾CardSO asset
                //一定要在Resources下
               
                Debug.Log("");
                for (int i = 0; i < cardSOs.Length; i++)
                {
                    Debug.Log(cardSOs[i].GetType());
                    if (cardSOs[i].GetType() != typeof(CardSO)) continue;
                    CardSO cardSO = (CardSO)cardSOs[i];
                    if (GUILayout.Button(cardSO.name,GUILayout.Width(divineWidth - 30)))
                    {
                        edittingCardSO = cardSO;
                        //建立Editor物件 
                        CardSOEditor = Editor.CreateEditor(edittingCardSO);
                    }
                }

                EditorGUILayout.EndScrollView();
            },GUILayout.Width(divineWidth));


            //右邊欄位
            VerticalGroup(() =>
            {
                EditorGUI.DrawRect(new Rect(divineWidth, 0, position.width - divineWidth, position.height),ColorSet.DarkGray);
                if(CardSOEditor != null)
                {
                    //呼叫Editor的OnInspectorGUI
                    CardSOEditor.OnInspectorGUI();
                    //標記SO為修改過，之後會自動存擋
                    EditorUtility.SetDirty(edittingCardSO);
                }
            });


            //分隔線作畫 1/4 3/4
            Rect divideLine = new Rect(divineWidth, 0, 2, position.height);
            EditorGUI.DrawRect(divideLine, Color.black);
        });
        

        
    }
}
