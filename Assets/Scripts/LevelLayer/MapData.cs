using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData 
{
    public List<LevelData> allLevels = new List<LevelData>();
    [SerializeField]
    private LevelData currentLevel = null;
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
    }
    public void NextLevel()
    {
        Debug.Log("Next Level");
        if (currentLevel.enemy == null)
        {
            Debug.Log("沒有層數，設定第一層");
            currentLevel = allLevels[0];
        }
        else
        {
            int level = currentLevel.Layer;
            level++;
            if (level > allLevels.Count)
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
        
        
    }

    public void setCurrentLevel(LevelData levelData)
    {
        currentLevel = levelData;
    }
}
