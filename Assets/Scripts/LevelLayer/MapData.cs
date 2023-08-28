using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData 
{
    
    public List<LevelData[]> allLevels = new List<LevelData[]>();
    public List<LayerData> allLayers = new List<LayerData>();

    public List<string> line_Info= new List<string>();

    [SerializeField]
    private LevelData currentLevel = null;

    [SerializeField]
    private int currentLayer = -1;

    public LevelData Currentlevel
    {
        get {
            if (currentLevel.enemy == null)
            {
                //Debug.Log("currentLevel為空");
                //NextLevel();
            }
            return currentLevel;
        }
        set { currentLevel = value; }
    }

    public int CurrentLayer
    {
        get
        {
            if(currentLayer < 0)
            {
                Debug.Log("currentLayer為負的");
            }
            return currentLayer;
        }
    }

    public MapData()
    {
        allLevels = new List<LevelData[]>();
        line_Info = new List<string>();
        currentLevel = null;
        currentLayer = -1;
    }

    /// <summary>
    /// 如果沒有層數就設置為第一層(0)，若有就回傳下一層的層數
    /// </summary>
    /// <returns></returns>
    public int NextLayer()
    {
        //Debug.LogFormat("now: {0} ,Next Layer",currentLayer);
        if (currentLayer < 0)
        {
            Debug.Log("沒有層數，設定第一層");
            currentLayer= 0;
        }
        else
        {
            currentLayer ++;
            Debug.Log(allLayers.Count);
            if(currentLayer > allLayers.Count)
            {
                Debug.Log("這是最後一層");
                return -1;
            }
        }
        return currentLayer;
    }


    /// <summary>
    /// 如果有下一層回傳true
    /// </summary>
    /// <returns></returns>
    /*
    public bool NextLevel()
    {
        
        Debug.Log("Next Level");
        if(currentLevel == null)
        {
            Debug.LogError("currentLevel is null");
        }
        if (currentLevel.enemy == null)
        {
            Debug.Log("沒有層數，設定第一層");
            currentLevel = allLevels[0];
        }
        else
        {
            int level = currentLevel.Layer;
            level++;
            if (level >= allLevels.Count)
            {
                Debug.Log("這是最後一層");
                return false;
            }
            foreach (LevelData item in allLevels)
            {
                if (item.Layer == level)
                {
                    this.currentLevel = item;
                }
            }
        }
        return true;
    }
    */

    public bool check_Layer()
    {
        return (currentLayer < 0);
    }

    /// <summary>
    /// 確認當前關卡是否是當前層
    /// </summary>
    /// <returns></returns>
    public bool check_Level()
    {
        if(currentLevel.enemy != null)
        {
            Debug.Log("currentLevel不為空");
            return (currentLevel.Layer == currentLayer);
        }
        else
        {
            return false; 
        }
    }


    /*
    public bool Check_Layer()
    {
        if (currentLevel.enemy == null) { 
            currentLevel = allLevels[0];
            Debug.LogFormat("現在層數 {0}", currentLevel.Layer);
            return true;
        }
        else if(currentLevel.Layer >= allLevels.Count)
        {
            return false;
        }
        else
        {
            currentLevel = allLevels[current_layer];
            Debug.LogFormat("現在層數 {0}", current_layer);
            return true;
        }

        
    }
    */
    public void setCurrentLevel(LevelData levelData)
    {
        currentLevel = levelData;
    }

    public void setCurrentLayer(int layer)
    {
        currentLayer = layer;
    }

    public LevelData getLevel()
    {
        return null;
    }

    public void Reset()
    {
        currentLevel = null;
    }
}
