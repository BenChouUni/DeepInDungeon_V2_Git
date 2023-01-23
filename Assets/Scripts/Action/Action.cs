using System;
using UnityEngine;

[Serializable]
public class Action 
{
    public int id;
    public int parameter;

    public Action(int _id,int _param)
    {
        this.id = _id;
        this.parameter = _param;
    }
}
