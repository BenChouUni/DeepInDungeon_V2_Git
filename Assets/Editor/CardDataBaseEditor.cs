using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardDataBase))]
public class CardDataBaseEditor : Editor
{
    static bool showInitialNum = false;
    static bool showActionList = false;
    private SerializedProperty cardDatasSerializedProperty;
    
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CardDataBase cardDataBase = (CardDataBase)target;
        List<CardData> cardDatas = new List<CardData>();

        cardDatasSerializedProperty = this.serializedObject.FindProperty("cardList");

        foreach (CardSO item in cardDataBase.cardList)
        {
            if (item == null)
            {
                continue;
            }
            cardDatas.Add(item.cardData);
        }

        for (int i = 0; i < cardDatas.Count; i++)
        {
            CreateCardDataRow(cardDatas, i);
            /*
            var cardDataProperty = cardDatasSerializedProperty.GetArrayElementAtIndex(i);

            var actionListProperty = cardDataProperty.FindPropertyRelative("cardAction");
            if (actionListProperty != null)
            {
                Debug.LogFormat("find card action{0}", i);

                // Update the serialize object
                this.serializedObject.Update();

                // Display properties
                EditorGUILayout.PropertyField(actionListProperty, true);

                // Apply modif
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                Debug.LogFormat("cannot find card action{0}", i);
            }
            
            */
            
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Add New CardSO"))
        {
            CardSO so = CreateCardSOAsset();
            cardDataBase.cardList.Add(so);
            Repaint();
        }

        if (GUILayout.Button("Save Name"))
        {
            foreach (CardSO item in cardDataBase.cardList)
            {
                SaveName(item);
                EditorUtility.SetDirty(item);
            }
            AssetDatabase.SaveAssets();
        }


    }

    private static void CreateCardDataRow(List<CardData> list,int i)
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
        list[i].cardName = EditorGUILayout.TextField(list[i].cardName);

        EditorGUILayout.LabelField("Cost", GUILayout.MaxWidth(30));
        list[i].cost = EditorGUILayout.IntField(list[i].cost);

        EditorGUILayout.EndHorizontal();
        
        showInitialNum = EditorGUILayout.Foldout(showInitialNum, "Show Initial Num",true);
        if (showInitialNum)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("InitailNum", GUILayout.MaxWidth(20));
            list[i].initialnum = EditorGUILayout.IntField(list[i].initialnum);
            EditorGUILayout.EndHorizontal();
        }

      

    }


    private static CardSO CreateCardSOAsset()
    {
        CardSO asset = ScriptableObject.CreateInstance<CardSO>();

        string path = "Assets/SO/CardSO/NewCardSO.asset";

        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        return asset;
    }

    private static void SaveName(CardSO item)
    {
        string assetPath = AssetDatabase.GetAssetPath(item.GetInstanceID());
        CardData cardData = item.cardData;

        CardData newData = new CardData(cardData.id,cardData.cardName,cardData.cost,cardData.initialnum);

        
        AssetDatabase.RenameAsset(assetPath, cardData.cardName);
        
        //會創建一個新的檔案，舊的會找不到，所以要重新給值



        assetPath = AssetDatabase.GetAssetPath(item.GetInstanceID());
        CardSO newSO = AssetDatabase.LoadAssetAtPath<CardSO>(assetPath);
        newSO.cardData = newData;

        AssetDatabase.SaveAssets();
    }
}
