using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattkeDistanceManager : MonoBehaviour
{
    public Text distanceText;

    public void ShowDistance(int distance)
    {
        distanceText.text = string.Format("{0}", distance);
    }
}
