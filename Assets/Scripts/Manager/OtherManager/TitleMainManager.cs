using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleMainManager : MonoBehaviour
{
    public void Click()
    {
        DataPersistenceManager.instance.NewGame();

        //data.playerData.setCharaterName(nameInput);
        //DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(0);
    }
}
