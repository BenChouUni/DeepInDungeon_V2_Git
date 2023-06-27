using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public GameObject LevelPanel;
    public GameObject now_Level;
    GameObject[] Levels;
    public int Layer;
    private int now_layer;

    private void Start()
    {
        now_layer = 0;
        Layer = LevelPanel.transform.childCount;
        change_layer(now_layer);
    }

    public void Fight()
    {
        //this.GetComponent<Button>().interactable = false;
        //myObjArray = GameObject.FindGameObjectsWithTag("level");
        //Debug.Log(myObjArray[0].GetComponent<Image>().color = Color.blue);
    }

    public void Click()
    {
        now_layer++;
        if(now_layer < Layer)
        {
            del_layer();
            change_layer(now_layer);
        }
    }

    public void change_layer(int _layer)
    {
        now_Level = LevelPanel.transform.GetChild(_layer).gameObject;
        for (int i = 0; i < now_Level.transform.childCount; i++)
        {
            now_Level.transform.GetChild(i).GetComponent<Button>().interactable = true;
            now_Level.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(Click);
        }
    }

    public void del_layer()
    {
        for (int i = 0; i < now_Level.transform.childCount; i++)
        {
            now_Level.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }
}
