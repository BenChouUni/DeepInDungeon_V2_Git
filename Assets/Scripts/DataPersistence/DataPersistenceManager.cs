using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoSingleton<DataPersistenceManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName ;
    [Header("Dir Path")]
    [SerializeField] private string dirPath;
    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    private void Awake()
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
    }
    /// <summary>
    /// 打開遊戲時會自動讀取
    /// </summary>
    private void Start()
    {
        fileName = "GameData";
        dirPath = Application.persistentDataPath;
        this.dataHandler = new FileDataHandler(dirPath, fileName);
        LoadGame();
        //ResetGame();
    }
    /// <summary>
    /// 程式跳出執行存擋
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        Debug.Log("New Game");
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No Data was found.Start default new game data");
            NewGame();
        }

        foreach (IDataPersistence item in dataPersistenceObjects)
        {
            item.LoadData(gameData);
        }
    }

    //確認是否有現有存檔
    public bool Check_Has_Date()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence item in dataPersistenceObjects)
        {
            item.SaveData(ref gameData);
        }

        dataHandler.Save(this.gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    private void ResetGame()
    {
        NewGame();
        SaveGame();
        LoadGame();
    }

    /// <summary>
    /// 進到下一關，在選完獎勵後按下前進按鈕觸發
    /// </summary>
    public void Next()
    {
        gameData.mapData.current_layer += 1;
        Debug.LogFormat("{0}", gameData.mapData.current_layer);
    }

    public int now_layer()
    {
        return gameData.mapData.current_layer;
    }
}
