using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class InputWindow : MonoBehaviour,IDataPersistence
{
    private TMP_InputField inputField;

    private string nameInput;

    private void Awake()
    {
        inputField = this.transform.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        Hide();
    }
    public void Show(string inputString)
    {
        gameObject.SetActive(true);
        if (inputField == null)
        {
            Debug.Log("text field is null");
            return;
        }
        inputField.text = inputString;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnOk()
    {
        Hide();
        nameInput = inputField.text;
        Debug.Log("輸入的名字是：" + nameInput);
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(0);

    }

    public void OnCancel()
    {
        Hide();
    }

    public void LoadData(GameData data)
    {
        // do nothing
    }

    public void SaveData(ref GameData data)
    {
        data.playerData.setCharaterName(nameInput);
    }
}
