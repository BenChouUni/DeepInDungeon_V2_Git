using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardDataBase))]
public class CardDataBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CardDataBase cardDataBase = (CardDataBase)target;
        List<CardData> cardDatas = new List<CardData>();

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
            }
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

        CardData newData = new CardData(cardData.id,cardData.cardName,cardData.cost);

        
        AssetDatabase.RenameAsset(assetPath, cardData.cardName);
        
        //會創建一個新的檔案，舊的會找不到，所以要重新給值



        assetPath = AssetDatabase.GetAssetPath(item.GetInstanceID());
        CardSO newSO = AssetDatabase.LoadAssetAtPath<CardSO>(assetPath);
        newSO.cardData = newData;

        AssetDatabase.SaveAssets();
    }
}
