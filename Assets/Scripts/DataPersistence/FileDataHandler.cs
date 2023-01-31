using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dirPath,string fileName)
    {
        this.dataDirPath = dirPath;
        this.dataFileName = fileName;
    }

    public GameData Load()
    {
        //結合位址跟檔名創建完整路徑
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error accur when trying to load data to file" + fullPath + "\n" + ex);
            }
        }

        return loadData;
    }

    public void Save(GameData data)
    {
        //結合位址跟檔名創建完整路徑
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log("Save to" + fullPath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        string dataToStore = JsonUtility.ToJson(data, true);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
        try
        {
            /*
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            */
        }
        catch (Exception ex)
        {
            Debug.LogError("Error accur when trying to save data to file" + fullPath + "\n" + ex);
        }
    }
}
