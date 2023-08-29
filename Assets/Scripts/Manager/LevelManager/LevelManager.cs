using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour,IDataPersistence
{

    public GameObject LevelPanel;
    public GameObject now_Level;
    public GameObject level_prefab;
    public GameObject layer_prefab;
    public List<EnemyGroupSO> EnemyGroups;

    public List<GameObject> Levels = new List<GameObject>();
    public int Layer;
    private int now_layer;

    public int[] Levelsnum;


    [Header("填入關卡（暫時）")]
    public MapData mapData;

    private void Start()
    {

        if (mapData == null)
        {
            Debug.LogError("map data is null");
            return;
        }


        if (mapData.CurrentLayer < 0)
        {
            Debug.Log("Create a new map");
            Levelsnum = MapManager.instance.CreateLevels(Layer);
            CreateLevelData();

        }
        CreateMap();


        //Layer = mapData.allLevels.Count;
        //MapManager.instance.CreateMap(Layer);

        //確認當前在哪一關
        /*
        if(!mapData.Check_Layer())
        {
            Debug.LogFormat("全數通關");
            SceneManager.LoadScene(3);
        }
        */

        if (mapData.check_Layer())
        {
            mapData.NextLayer();
        }
        now_layer = mapData.CurrentLayer;
        /*
        if (!mapData.check_Ini())
        {
            mapData.NextLevel();
        }
        */
        //mapData.NextLevel();
        if (mapData.Currentlevel == null)
        {
            
        }
        //若目前的level是空，或與當前層數不合則切換層數按鈕
        if(!mapData.check_Level())
        {
            foreach(GameObject level in Levels)
            {
                if(level.GetComponent<LevelInfo>().levelData.Layer == mapData.CurrentLayer)
                {
                    level.transform.GetComponent<Button>().interactable = true;
                }
                else
                {
                    level.transform.GetComponent<Button>().interactable = false;
                }
            }
            now_layer = mapData.Currentlevel.Layer;
        }
        else
        {
            foreach (GameObject level in Levels)
            {
                if (level.GetComponent<LevelInfo>().levelData.Layer == mapData.Currentlevel.Layer)
                {
                    if (level.GetComponent<LevelInfo>().levelData.number == mapData.Currentlevel.number)
                    {
                        level.transform.GetComponent<Button>().interactable = true;
                        break;
                    }
                }
            }
            now_layer = mapData.Currentlevel.Layer;
        }
        //Levels[now_layer].transform.GetComponent<Button>().interactable = true;
        //Layer = LevelPanel.transform.childCount;
        //change_layer(now_layer);
        //CreateMap();
    }

    public void Fight()
    {
        var now_level = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        mapData.Currentlevel = now_level.GetComponent<LevelInfo>().levelData;
        SceneManager.LoadScene(1);
        Debug.Log("存檔");
        DataPersistenceManager.instance.SaveGame();
        //this.GetComponent<Button>().interactable = false;
        //myObjArray = GameObject.FindGameObjectsWithTag("level");
        //Debug.Log(myObjArray[0].GetComponent<Image>().color = Color.blue);
    }

    /// <summary>
    /// 目前未用到，待修
    /// </summary>
    public void OnClick()
    {
        //transform.GetComponent<LevelInfo>().levelData.enemy
        Debug.Log(GetComponent<LevelInfo>().levelData.EnemyGroup.groupName);
        Debug.Log("OnClick");
        mapData.Currentlevel = this.GetComponent<LevelInfo>().levelData;
        //JClone.DeepClone<LevelData>(this.GetComponent<LevelInfo>().levelData);
        //now_layer++;
        if (now_layer < Layer)
        {
            del_layer();
            change_layer(now_layer);
        }
        Fight();
    }

    public void change_layer(int _layer)
    {
        now_Level = LevelPanel.transform.GetChild(_layer).gameObject;
        for (int i = 0; i < now_Level.transform.childCount; i++)
        {
            now_Level.transform.GetChild(i).GetComponent<Button>().interactable = true;
            now_Level.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(OnClick);
        }
    }

    public void del_layer()
    {
        for (int i = 0; i < now_Level.transform.childCount; i++)
        {
            now_Level.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }

    public void LoadData(GameData data)
    {
        //只儲存currentLevel
        if (data.mapData != null)
        {
            this.mapData.setCurrentLevel(data.mapData.Currentlevel);

        }

        //改成讀取全部
        this.mapData = data.mapData;

    }

    public void SaveData(ref GameData data)
    {
        data.mapData = this.mapData;
    }

    public void CreateMap()
    {
        for(int i = 0; i < Layer; i++)
        {
            GameObject new_layer;
            new_layer = Instantiate(layer_prefab, LevelPanel.transform, false);
            for (int j = 0; j < mapData.allLayers[i].this_layer_Levels.Count; j++)
            {
                GameObject new_level;
                new_level = Instantiate(level_prefab, new_layer.transform, false);
                new_level.transform.GetComponent<Button>().onClick.AddListener(Fight);
                new_level.GetComponent<LevelInfo>().levelData = mapData.allLayers[i].this_layer_Levels[j];
                Levels.Add(new_level);
            }
//            Debug.Log("生成地圖");
            
            //new_level.transform.GetComponent<Button>().onClick.AddListener(OnClick);
            
            
        }
    }

    public void CreateLevelData()
    {
        for(int i = 0; i < Layer; i++)
        {
            mapData.allLayers.Add(new LayerData(i, new List<LevelData>()));
            for (int j = 0; j < Levelsnum[i]; j++)
            {
                LevelData leveldata = new LevelData(i, j, EnemyGroups[0]);
                mapData.allLayers[i].this_layer_Levels.Add(leveldata);
            }
        }
    }
}
