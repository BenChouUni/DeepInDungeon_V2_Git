using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class LevelManager : MonoBehaviour,IDataPersistence
{

    public GameObject LevelPanel;
    public GameObject now_Level;
    public GameObject level_prefab;
    public GameObject layer_prefab;
    public List<EnemySO> EnemyType;
    public GameObject Line_prefab;
    public Transform Line_Panel;
    public List<EnemyGroupSO> EnemyGroups;

    public List<List<GameObject>> Levels = new List<List<GameObject>>();
    public int Layer;
    private int now_layer;

    public int[] Levelsnum;


    public List<string> _lineInfo = new List<string>();


    [Header("填入關卡（暫時）")]

    public MapData mapData = new MapData();

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
            Levelsnum = MapDisplay.instance.CreateLevels(Layer);
            _lineInfo = MapDisplay.instance.CreateLine(Levelsnum, Layer);
            foreach(string line in _lineInfo)
            {
                Debug.Log(line);
            }
            mapData.line_Info = _lineInfo;
            CreateLevelData();

        }
        CreateMap();
        CreateLine();


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
        /*
        if (mapData.Currentlevel == null)
        {
            
        }
        */

        switch_level();

        //若目前的level是空，或與當前層數不合則切換層數按鈕
        /*
        if(!mapData.check_Level())
        {
            for(int i = 0; i < Layer; i++)
            {
                foreach (GameObject level in Levels[i])
                {
                    if (level.GetComponent<LevelInfo>().levelData.Layer == mapData.CurrentLayer)
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
        }
        else
        {
            for (int i = 0; i < Layer; i++)
            {
                foreach (GameObject level in Levels[i])
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
        }
        */
        //Levels[now_layer].transform.GetComponent<Button>().interactable = true;
        //Layer = LevelPanel.transform.childCount;
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
    /*
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
    */

    public void switch_level()
    {
        if (mapData.Currentlevel.EnemyGroup == null)
        {
            foreach (GameObject level in Levels[0])
            {
                level.transform.GetComponent<Button>().interactable = true;
            }
        }
        else if (mapData.Currentlevel.EnemyGroup != null)
        {
            LevelData _leveldata = mapData.Currentlevel;
            if (!mapData.check_Level())
            {
                if (_leveldata.Layer + 1 < Layer) {
                    //the first line
                    if (mapData.line_Info[_leveldata.Layer][_leveldata.number] != '*')
                    {
                        GameObject level = Levels[_leveldata.Layer + 1][int.Parse(mapData.line_Info[_leveldata.Layer][_leveldata.number].ToString())];
                        level.transform.GetComponent<Button>().interactable = true;
                    }
                    //the second line
                    string _line = mapData.line_Info[Layer + _leveldata.Layer + 1];
                    for (int i = 0; i < _line.Length; i++)
                    {
                        if (_line[i] != '*')
                        {
                            if (int.Parse(_line[i].ToString()) == _leveldata.number)
                            {
                                GameObject level = Levels[_leveldata.Layer + 1][i];
                                level.transform.GetComponent<Button>().interactable = true;
                            }

                        }
                    }
                }
                else
                {
                    GameObject level = Levels[_leveldata.Layer][_leveldata.number];
                    if (level != null)
                    {
                        level.transform.GetComponent<Button>().interactable = true;
                    }
                }
            } 
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
            Levels.Add(new List<GameObject>());
            GameObject new_layer;
            new_layer = Instantiate(layer_prefab, LevelPanel.transform, false);
            for (int j = 0; j < mapData.allLayers[i].this_layer_Levels.Count; j++)
            {
                GameObject new_level;
                new_level = Instantiate(level_prefab, new_layer.transform, false);
                new_level.transform.GetComponent<Button>().onClick.AddListener(Fight);
                new_level.GetComponent<LevelInfo>().levelData = mapData.allLayers[i].this_layer_Levels[j];
                Levels[i].Add(new_level);
            }
//            Debug.Log("生成地圖");
            
            //new_level.transform.GetComponent<Button>().onClick.AddListener(OnClick);
            
            
        }
    }

    public void CreateLine()
    {
        for(int i = 0; i < Layer; i ++)
        {
            for(int j = 0; j < mapData.line_Info[i].Length; j++)
            {
                if (mapData.line_Info[i][j] != '*')
                {
                    GameObject new_line;
                    new_line = Instantiate(Line_prefab, Line_Panel, false);
                    RectTransform[] connect = new RectTransform[2] { Levels[i][j].GetComponent<RectTransform>(), Levels[i+1][int.Parse(mapData.line_Info[i][j].ToString())].GetComponent<RectTransform>() };
                    new_line.GetComponent<UILineConnector>().transforms = connect;
                }
            }
        }

        for (int i = 0; i < Layer; i++)
        {
            for (int j = 0; j < mapData.line_Info[i].Length; j++)
            {
                if (mapData.line_Info[i + Layer][j] != '*')
                {
                    //Debug.LogFormat("{0}, {1}", mapData.line_Info[i + Layer][j], Levels[i - 1].Count);
                    if (int.Parse(mapData.line_Info[i + Layer][j].ToString()) < Levels[i - 1].Count)
                    {
                        //Debug.LogFormat("{0}, {1}", mapData.line_Info[i + Layer][j], Levels[i - 1].Count);
                        GameObject new_line;
                        new_line = Instantiate(Line_prefab, Line_Panel, false);
                        RectTransform[] connect = new RectTransform[2] { Levels[i][j].GetComponent<RectTransform>(), Levels[i - 1][int.Parse(mapData.line_Info[i + Layer][j].ToString())].GetComponent<RectTransform>() };
                        new_line.GetComponent<UILineConnector>().transforms = connect;
                    }
                    else
                    {
                        GameObject new_line;
                        new_line = Instantiate(Line_prefab, Line_Panel, false);
                        RectTransform[] connect = new RectTransform[2] { Levels[i][j].GetComponent<RectTransform>(), Levels[i - 1][Levels[i - 1].Count - 1].GetComponent<RectTransform>() };
                        new_line.GetComponent<UILineConnector>().transforms = connect;
                    }
                }
            }
        }
    }

    public void CreateLevelData()
    {
        for(int i = 0; i < Layer; i++)
        {
            mapData.allLayers.Add(new LayerData(i, new List<LevelData>()));
            for (int j = 0; j < Levelsnum[i]; j++)
            {
                LevelData leveldata = new LevelData(i, j, LevelType.Enemy, EnemyGroups[0].Enemygroupdata);
                mapData.allLayers[i].this_layer_Levels.Add(leveldata);
            }
        }
    }
}
