using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMainManager : MonoBehaviour
{
    public GameObject LoadGame_Button;
    private void Start()
    {
        //確認有沒有存檔
        if (!DataPersistenceManager.instance.Check_Has_Date())
        {
            LoadGame_Button.GetComponent<Button>().interactable = false;
            UnityEngine.Object.DestroyImmediate(LoadGame_Button.GetComponent<ZoomInUI>());
        }
        else
        {
            LoadGame_Button.GetComponent<Button>().interactable = true;
            LoadGame_Button.GetComponent<ZoomInUI>().ZoomSize = 1.2f;
        }
    }

    public void New()
    {
        DataPersistenceManager.instance.NewGame();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(5);
    }

    public void Load()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Debug.Log("退出遊戲");
        //Application.Quit();
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
