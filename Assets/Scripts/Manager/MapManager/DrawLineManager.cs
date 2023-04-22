using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    static LineRenderer line;
    public Transform startpoint;
    public Transform endpoint;
    //public GameObject Map;
    void Start()
    {
        line= GetComponent<LineRenderer>();
    }

    public static void Setpoint(int[]points)
    {
        int sum = 0;
        for(int i = 0; i < points.Length; i++)
        {
            sum += points[i];
        }
        //DrawLineManager.line.SetPositions = sum;
    }

    public static void Draw(Vector3 point1, Vector3 point2)
    {
        Debug.Log(point1);
        Debug.Log(point2);
        line.SetPosition(0, point1);
        line.SetPosition(1, point2);
    }
    // Update is called once per frame
    void Update()
    {
        //line.SetPosition(0, startpoint.position);
        //line.SetPosition(1, endpoint.position);
    }
}
