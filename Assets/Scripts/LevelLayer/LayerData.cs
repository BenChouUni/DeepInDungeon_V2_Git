using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerData
{
    public int Layer;
    List<LevelData> Levels = new List<LevelData>();
}
