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

    public  List<Transform> HandCardList;

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

    void Update()
    {
        bool stillmoving = false;
        for(int i = 0; i < HandCardList.Count; i++)
        {
            if (HandCardList[i].GetComponent<CardMoveUI>().moving)
            {
                stillmoving = true;
            }
        }
        if (stillmoving)
        {
            CanInPointer = false;
        }
        else
        {
            CanInPointer = true;
            Dispersenow = false;
            Gathernow = false;
        }
    }

    public void AddHandCard(Transform handCard)
    {
        HandCardList.Add(handCard);
        handCard.position = new Vector3(handCard.position.x + 800f, handCard.position.y + 100f, handCard.position.z);
        //handCard.localScale = Vector2.one* 0.4f;
        //Debug.Log(handCard.GetComponent<CardMoveUI>().moving);

        SetLayout();
    }

    public void RemoveHandCard(Transform handCard)
    {
        HandCardList.Remove(handCard);
        //Debug.Log("Remove");
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

        for(int i = 0 ; i < HandCardList.Count; i++)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, i / (HandCardList.Count - 1f));
            TargetPosition.Add(new Vector3(Mathf.Cos(angle) * radius + 960f, Mathf.Sin(angle) * radius - radius + 150f, 1f));

            TargetRotation.Add(Quaternion.Euler(0f, 0f, Mathf.Lerp(8f, -8f, i / (HandCardList.Count - 1f))));

            HandCardList[i].GetComponent<CardMoveUI>().moving = true;
            HandCardList[i].GetComponent<CardMoveUI>().Destination = TargetPosition[i];
            

            //HandCardList[i].position = TargetPosition[i];
            HandCardList[i].rotation = TargetRotation[i];
        }
    }

    private void SetLineLayout()
    {
        TargetPosition.Clear();
        TargetRotation.Clear();
        float positionX = (1f - HandCardList.Count) * 100f / 2f;
        if(HandCardList.Count == 1)
        {
            TargetPosition.Add(new Vector3(960f, 150f, 1f));
            TargetRotation.Add(Quaternion.Euler(Vector3.zero));
            HandCardList[0].GetComponent<CardMoveUI>().moving = true;
            HandCardList[0].GetComponent<CardMoveUI>().Destination = TargetPosition[0];
            //HandCardList[0].position = TargetPosition[0];
            HandCardList[0].rotation = TargetRotation[0];
        }
        else
        {
            for(int i = 0; i < HandCardList.Count; i++)
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
    
    
    public void Disperse(Vector3 move)   
    {
        //Debug.Log("Disperse");
        Dispersenow = true;
        int pos = 0;
        //先尋找位置
        //若是牌在執行Gather時，會找不到位置
        if (Gathernow)
        {
            //判斷鼠標示向左還是向右
            if (move.x - HandCardList[Nowpointer].position.x >= 0)
            {
                for (int i = Nowpointer; i < HandCardList.Count; i++)
                {
                    if (Mathf.Abs(TargetPosition[i].x - move.x) <= DisperseRadius)
                    {
                        pos = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = Nowpointer; i >= 0; i--)
                {
                    if (Mathf.Abs(TargetPosition[i].x - move.x) <= DisperseRadius)
                    {
                        pos = i;
                        break;
                    }
                }
            }
            Gathernow = false;
        }
        else
        {
            for (int i = 0; i < HandCardList.Count; i++)
            {
                if (TargetPosition[i].x == move.x)
                {
                    pos = i;
                    break;
                }
            }
        }
        Nowpointer = pos;
        //Debug.Log(pos);
        for (int i = pos - 2; i <= pos + 2; i++)
        {
            if (i >= 0 && i < HandCardList.Count && i != pos)
            {
                
                HandCardList[i].GetComponent<CardMoveUI>().moving = true;
                HandCardList[i].GetComponent<CardMoveUI>().Destination = new Vector3(TargetPosition[i].x - DisperseRadius * (1f / (pos - i)), TargetPosition[i].y, TargetPosition[i].z);
                
                //HandCardList[i].position = new Vector3(TargetPosition[i].x - DisperseRadius * (1f / (pos - i)), TargetPosition[i].y, TargetPosition[i].z);
            }
            else if (i == pos)
            {
                
                HandCardList[i].GetComponent<CardMoveUI>().moving = true;
                HandCardList[i].GetComponent<CardMoveUI>().Destination = new Vector3(TargetPosition[i].x, 250f, 1f);
                
                //HandCardList[i].position= new Vector3(TargetPosition[i].x, 250f, 1f);
                HandCardList[i].rotation = (Quaternion.Euler(0f, 0f, 0f));

            }
        }

    }

    /// <summary>
    /// 手牌聚攏
    /// </summary>
    /// <param name="move"></param>
    public void Gather(Vector3 move)
    {
        //Debug.Log("Gather");
        Gathernow = true;
        Dispersenow = false;
        int pos = 0;
        //先尋找位置
        pos = Nowpointer;    //最後離開的位置即為收攏的中間那張牌
        //Debug.Log(pos);

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
    }
}

