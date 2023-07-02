using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMainManager : MonoBehaviour
{
    public void Click()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene(0);
    }
}
