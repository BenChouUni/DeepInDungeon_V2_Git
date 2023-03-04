using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AwardCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(1, 3)]
    public float ZoomSize;

    private bool isOn = false;
    void Update()
    {
        if (isOn && !AwardMainManager.instance.Choosen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(this.GetComponent<CardDisplay>().CardData.id);
                AwardMainManager.instance.AddCardToDeck(this.GetComponent<CardDisplay>().CardData);
            }
        }
        /*
        if (AwardMainManager.instance.Choosen)
        {
            Destroy(this, 0);
        }
        */
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = Vector2.one * ZoomSize;
        isOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOn = false;
        this.transform.localScale = Vector2.one;
    }
}
