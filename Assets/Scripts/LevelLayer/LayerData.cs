using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerData
{
    public int Layer;
    public List<LevelData> this_layer_Levels = new List<LevelData>();

    public LayerData(int layer, List<LevelData> leveldata) {
        this.Layer = layer;
        this.this_layer_Levels = leveldata;
    }
}
