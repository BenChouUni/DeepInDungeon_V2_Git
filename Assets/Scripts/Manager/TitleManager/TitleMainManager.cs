using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMainManager : MonoBehaviour
{
    public void Click()
    {
        SceneManager.LoadScene(0);
    }
}
