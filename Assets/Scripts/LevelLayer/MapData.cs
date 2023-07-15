using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData 
{
    public List<LevelData> allLevels = new List<LevelData>();
    [SerializeField]
    private LevelData currentLevel = null;
    public int current_layer;
    public LevelData Currentlevel
    {
        get {
            if (currentLevel.enemy == null)
            {
                NextLevel();
            }
            return currentLevel;
        }
    }

    public MapData()
    {
        allLevels = new List<LevelData>();
        currentLevel = null;
        current_layer = -1;
    }
    public void NextLevel()
    {
        /*
        Debug.Log("Next Level");
        if (currentLevel.enemy == null)
        {
            Debug.Log("沒有層數，設定第一層");
            currentLevel = allLevels[0];
            current_layer = 0;
        }
        else
        {
            int level = currentLevel.Layer;
            level++;
            if (level >= allLevels.Count)
            {
                Debug.Log("there is no layer");
                return;
            }
            foreach (LevelData item in allLevels)
            {
                if (item.Layer == level)
                {
                    this.currentLevel = item;
                }
            }
        }
        */
        current_layer += 1;
        
        
    }

    public bool Check_Layer()
    {
        current_layer = DataPersistenceManager.instance.now_layer();
        if (current_layer < 0) { 
            current_layer= 0;
            currentLevel = allLevels[0];
            Debug.LogFormat("現在層數 {0}", current_layer);
            return true;
        }
        else if(current_layer >= allLevels.Count)
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
    public void setCurrentLevel(LevelData levelData)
    {
        currentLevel = levelData;
    }
}
