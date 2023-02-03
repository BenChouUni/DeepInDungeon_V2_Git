using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardZoneDisplay : MonoBehaviour
{
    public Text counter;

    public void ShowCounter(int count)
    {
        counter.text = string.Format("{0}", count);
    }
}
