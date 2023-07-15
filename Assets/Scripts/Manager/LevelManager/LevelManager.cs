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

    public List<GameObject> Levels = new List<GameObject>();
    //GameObject[] Levels;
    public int Layer;
    private int now_layer;

    public MapData mapData;

    private void Start()
    {
        Layer = mapData.allLevels.Count;
        CreateMap();

        if (mapData == null)
        {
            Debug.LogError("map data is null");
            return;
        }
        //確認當前在哪一關
        /*
        if(!mapData.Check_Layer())
        {
            Debug.LogFormat("全數通關");
            SceneManager.LoadScene(3);
        }
        */

        if (!mapData.check_Ini())
        {
            mapData.NextLevel();
        }
        //mapData.NextLevel();
        if (mapData.Currentlevel == null)
        {
            
        }
        now_layer = mapData.Currentlevel.Layer;
        Levels[now_layer].transform.GetComponent<Button>().interactable = true;
        //Layer = LevelPanel.transform.childCount;
        //change_layer(now_layer);
        //CreateMap();
    }

    public void Fight()
    {
        SceneManager.LoadScene(1);
        Debug.Log("存檔");
        DataPersistenceManager.instance.SaveGame();
        //this.GetComponent<Button>().interactable = false;
        //myObjArray = GameObject.FindGameObjectsWithTag("level");
        //Debug.Log(myObjArray[0].GetComponent<Image>().color = Color.blue);
    }

    public void OnClick()
    {
        //now_layer++;
        if(now_layer < Layer)
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

    }

    public void SaveData(ref GameData data)
    {
        data.mapData = this.mapData;
    }

    public void CreateMap()
    {
        for(int i = 0; i < Layer; i++)
        {
//            Debug.Log("生成地圖");
            GameObject new_level;
            
            GameObject new_layer;
            new_layer = Instantiate(layer_prefab, LevelPanel.transform, false);
            new_level = Instantiate(level_prefab, new_layer.transform, false);
            //new_level.transform.GetComponent<Button>().onClick.AddListener(OnClick);
            new_level.transform.GetComponent<Button>().onClick.AddListener(Fight);
            Levels.Add(new_level);
        }
    }
}
