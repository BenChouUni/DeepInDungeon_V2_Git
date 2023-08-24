using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardsLayoutManager : MonoSingleton<CardsLayoutManager>
{
    [Range(1f, 5f)]
    public float Angle;

    [Range(1000f, 2500f)]
    public float radius;

    [Range(50f, 200f)]
    public float DisperseRadius;

    public List<Transform> HandCardList;
    

    public List<Vector3> TargetPosition;
    public List<Quaternion> TargetRotation;


    public float Speed = 10f;
    public bool moving = false;
    public float timer = 0f;
    public float alltimer = 0f;
    public bool CanInPointer = true;
    public bool Dispersenow = false;
    public bool Gathernow = false;
    public int Nowpointer;
    public int CardGoBack = 0;
    public bool Nowdragging = false;

    void Update()
    {
        /*
        bool stillmoving = false;

        //判斷是否還有手牌在移動
        for(int i = 0; i < HandCardList.Count; i++)
        {
            if (HandCardList[i].GetComponent<CardMoveUI>().moving)
            {
                stillmoving = true;
                break;
            }
        }

        //若有牌正在移動或是返回手牌，皆不可感應碰觸到手牌
        if (stillmoving && CardGoBack == 0)
        {
            CanInPointer = false;
        }
        else
        {
            CanInPointer = true;
            Dispersenow = false;
            Gathernow = false;
        }
        */
    }

    public void AddHandCard(Transform handCard)
    {
        HandCardList.Add(handCard);
        handCard.position = new Vector3(handCard.position.x + 800f, handCard.position.y + 100f, handCard.position.z);
        //handCard.localScale = Vector2.one* 0.4f;
        //Debug.Log(handCard.GetComponent<CardMoveUI>().moving);

        set_HandCard_Index();

        SetLayout();
    }

    public void RemoveHandCard(Transform handCard)
    {
        HandCardList.Remove(handCard);
        //Debug.Log("Remove");

        set_HandCard_Index();

        SetLayout();
    }

    public void SetLayout()
    {
        //Debug.Log("SetLayout");
        if (HandCardList.Count > 1)
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
        TargetPosition.Clear();
        TargetRotation.Clear();
        float startAngle;
        float endAngle;
        if (HandCardList.Count <= 10)
        {
            startAngle = Mathf.PI * (90f / 180f + (Angle * (HandCardList.Count) / 180f));
            endAngle = Mathf.PI * (90f / 180f - (Angle * (HandCardList.Count) / 180f));
        }
        else
        {
            startAngle = Mathf.PI * (90f / 180f + (Angle * 10 / 180f));
            endAngle = Mathf.PI * (90f / 180f - (Angle * 10 / 180f));
        }

        for (int i = 0; i < HandCardList.Count; i++)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, i / (HandCardList.Count - 1f));
            TargetPosition.Add(new Vector3(Mathf.Cos(angle) * radius + 960f, Mathf.Sin(angle) * radius - radius + 150f, 1f));

            TargetRotation.Add(Quaternion.Euler(0f, 0f, Mathf.Lerp(8f, -8f, i / (HandCardList.Count - 1f))));

            /*
            HandCardList[i].GetComponent<CardMoveUI>().moving = true;
            HandCardList[i].GetComponent<CardMoveUI>().Destination = TargetPosition[i];
            */


            HandCardList[i].position = TargetPosition[i];
            HandCardList[i].rotation = TargetRotation[i];
        }
    }

    private void SetLineLayout()
    {
        TargetPosition.Clear();
        TargetRotation.Clear();
        float positionX = (1f - HandCardList.Count) * 100f / 2f;
        if (HandCardList.Count == 1)
        {
            TargetPosition.Add(new Vector3(960f, 150f, 1f));
            TargetRotation.Add(Quaternion.Euler(Vector3.zero));
            /*
            HandCardList[0].GetComponent<CardMoveUI>().moving = true;
            HandCardList[0].GetComponent<CardMoveUI>().Destination = TargetPosition[0];
            */
            HandCardList[0].position = TargetPosition[0];
            HandCardList[0].rotation = TargetRotation[0];
        }
        else
        {
            for (int i = 0; i < HandCardList.Count; i++)
            {
                TargetPosition.Add(new Vector3(Mathf.Lerp(positionX, -positionX, i / (HandCardList.Count - 1f)) + 960f, 150f, 1f));
                TargetRotation.Add(Quaternion.Euler(Vector3.zero));
                HandCardList[i].position = TargetRotation[i] * TargetPosition[i];

            }
        }
    }

    /// <summary>
    /// 手牌散開
    /// </summary>
    /// <param name="move">當前卡牌位置</param>
    public void Disperse(int index, Vector3 mid_pos)   
    {
        Vector3 MiddleCard_pos = HandCardList[index].position;
        //HandCardList[index].GetComponent<CardMoveUI>().stop_Move();
        HandCardList[index].GetComponent<CardMoveUI>().call_Move(MiddleCard_pos, new Vector3(TargetPosition[index].x, 250f, 1f));
        HandCardList[index].rotation = (Quaternion.Euler(0f, 0f, 0f));

        //HandCardList[index].GetComponent<CardMoveUI>()


        //HandCardList[i].GetComponent<CardMoveUI>().moving = true;
        //HandCardList[i].GetComponent<CardMoveUI>().Destination = new Vector3(TargetPosition[i].x - DisperseRadius * (1f / (pos - i)), TargetPosition[i].y, TargetPosition[i].z);
        for(int i = index - 2; i <= index + 2; i++)
        {
            if(i >= 0 && i < HandCardList.Count && i != index) {
                HandCardList[i].GetComponent<CardMoveUI>().call_Move(HandCardList[i].position, new Vector3(TargetPosition[i].x - DisperseRadius * (1f / (index - i)), TargetPosition[i].y, TargetPosition[i].z));
            }
        }
        //HandCardList[i].position = new Vector3(TargetPosition[i].x - DisperseRadius * (1f / (pos - i)), TargetPosition[i].y, TargetPosition[i].z);


        //HandCardList[i].GetComponent<CardMoveUI>().moving = true;
        //HandCardList[i].GetComponent<CardMoveUI>().Destination = new Vector3(TargetPosition[i].x, 250f, 1f);

        //HandCardList[i].position= new Vector3(TargetPosition[i].x, 250f, 1f);
        //HandCardList[i].rotation = (Quaternion.Euler(0f, 0f, 0f));


    }
    

    /// <summary>
    /// 手牌聚攏
    /// </summary>
    /// <param name="move"></param>
    public void Gather(int index, Vector3 mid_pos)
    {
        //Debug.Log("Gather");
        //HandCardList[index].GetComponent<CardMoveUI>().stop_Move();

        HandCardList[index].GetComponent<CardMoveUI>().call_Move(mid_pos, TargetPosition[index]);
        HandCardList[index].rotation = (TargetRotation[index]);

        for(int i =0; i < HandCardList.Count; i++)
        {
            HandCardList[i].GetComponent<CardMoveUI>().call_Move(HandCardList[i].position, TargetPosition[i]);
        }
        /*
        //移動左右各兩張卡片
        for (int i = pos - 2; i <= pos + 2; i++)
        {
            if (i >= 0 && i < HandCardList.Count && i != pos)
            {
                
                HandCardList[i].GetComponent<CardMoveUI>().moving = true;
                HandCardList[i].GetComponent<CardMoveUI>().Destination = TargetPosition[i];
                
                //HandCardList[i].position = TargetPosition[i];
            }
            else if (i == pos)
            {
                
                HandCardList[i].GetComponent<CardMoveUI>().moving = true;
                HandCardList[i].GetComponent<CardMoveUI>().Destination = TargetPosition[i];
                
                //HandCardList[i].position = TargetPosition[i];
                HandCardList[i].rotation = TargetRotation[i];
            }
        }
        */
    }

    
    //設置手上每張牌的索引值，方便判斷是哪張牌
    public void set_HandCard_Index()
    {
        for(int i = 0; i < HandCardList.Count; i++)
        {
            HandCardList[i].GetComponent<HandCardUI>().hand_index = i;
        }
    }
}

