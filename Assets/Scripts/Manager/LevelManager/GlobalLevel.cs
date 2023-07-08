using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalLevel
{
    private static LevelData currentLevel;
    public static LevelData CurrentLevel
    {
        get { return currentLevel; }
    }

    public static void setLevel(LevelData data)
    {
        currentLevel = data;
    }

}
