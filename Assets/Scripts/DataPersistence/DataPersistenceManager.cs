using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoSingleton<DataPersistenceManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "GameData";
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
        dirPath = Application.persistentDataPath;
        this.dataHandler = new FileDataHandler(dirPath, fileName);
        LoadGame();
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
}
