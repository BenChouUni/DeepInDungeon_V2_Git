using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCquitManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            #if UNITY_STANDALONE
                        Application.Quit();
            #endif
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
