using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardsLayoutManager : MonoSingleton<CardsLayoutManager>
{
    [Range(1f, 10f)]
    public float Angle;
    //public List<CardSO> CardDeck;
    //public GameObject CardPrefab;
    //private Transform HandCardPanel;
    public  List<Transform> HandCardList;
    public List<Vector3> TargetPosition;
    public List<Quaternion> TargetRotation;


    /*
    public void Draw_Card_On_Hand(int id)
    {
        CardData cardData = GetCardData(id);
        GameObject new_card;

        if (cardData != null)
        {
            new_card = Instantiate(CardPrefab, HandCardPanel, false);
            new_card.GetComponent<CardDisplay>().CardData = cardData;
            HandCardList.Add(new_card.transform);
            SetLayout();
        }
    }

    public CardData GetCardData(int id)
    {
        foreach (CardSO item in CardDeck)
        {
            if (item.cardData.id == id)
            {
                return item.cardData;
            }
        }
        return null;
    }*/

    public void AddHandCard(Transform handCard)
    {
        HandCardList.Add(handCard);
        SetLayout();
    }

    public void RemoveHandCard(Transform handCard)
    {
        HandCardList.Remove(handCard);
        SetLayout();
    }

    public void SetLayout()
    {
        if(HandCardList.Count > 1)
        {
            SetCircleLayout();
        }
        else
        {
            SetLineLayout();
        }
    }

    private void SetCircleLayout()
    {
        Debug.Log("SetCircleLayout");
        TargetPosition.Clear();
        TargetRotation.Clear();
        float startAngle;
        float endAngle;
        if (HandCardList.Count <= 10)
        {
            startAngle = Mathf.PI * (90f / 180f + (Angle * (HandCardList.Count) / 180f));
            //Debug.Log(startAngle);
            endAngle = Mathf.PI * (90f / 180f - (Angle * (HandCardList.Count) / 180f));
            //Debug.Log(endAngle);
        }
        else
        {
            startAngle = Mathf.PI * (120f / 180f);
            // Debug.Log(startAngle);
            endAngle = Mathf.PI * (60f / 180f);
            //Debug.Log(endAngle);
        }
        float radius = 1500f;


            
        for(int i = 0 ; i < HandCardList.Count; i++)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, i / (HandCardList.Count - 1f));
            //Debug.Log(angle);
            TargetPosition.Add(new Vector3(Mathf.Cos(angle) * radius + 960f, Mathf.Sin(angle) * radius - radius + 150f, 1f));
            //Debug.Log(TargetPosition[i]);
            //TargetRotation.Add(Quaternion.Euler(0f, Mathf.Lerp(15f, -15f, i / (HandCardList.Count - 1f)), 0f));
            //TargetRotation.Add(Quaternion.Euler(0f, 0f, 90f));
            TargetRotation.Add(Quaternion.Euler(0f, 0f, Mathf.Lerp(8f, -8f, i / (HandCardList.Count - 1f))));
            HandCardList[i].position = TargetPosition[i];
            HandCardList[i].rotation = TargetRotation[i];
            //Debug.Log(HandCardList[i].position);
        }


    }

    private void SetLineLayout()
    {
        //Debug.Log("SetLineLayout");
        TargetPosition.Clear();
        TargetRotation.Clear();
        float positionX = (1f - HandCardList.Count) * 100f / 2f;
        if(HandCardList.Count == 1)
        {
            TargetPosition.Add(new Vector3(960f, 250f, 1f));
            TargetRotation.Add(Quaternion.Euler(Vector3.zero));
            //HandCardList[0].position = TargetPosition[0];
            HandCardList[0].position = TargetRotation[0] * TargetPosition[0];
            //Debug.Log(HandCardList[0].position);
        }
        else
        {
            for(int i = 0; i < HandCardList.Count; i++)
            {
                TargetPosition.Add(new Vector3(Mathf.Lerp(positionX, -positionX, i / (HandCardList.Count - 1f)) + 960f, 250f, 1f));
                TargetRotation.Add(Quaternion.Euler(Vector3.zero));
                //HandCardList[i].position = TargetPosition[i];
                HandCardList[i].position = TargetRotation[i] * TargetPosition[i];
                //Debug.Log(HandCardList[i].position);
            }
        }
    }
}

