using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMoveUI : MonoBehaviour
{
    
    public bool moving = false;
    public Vector3 original_pos;
    public Vector3 destination_pos;

    public bool bedragging = false;
    public Vector3 Destination;

    /*
    void Update()
    {
        if (moving && !bedragging)
        {
            //Debug.Log(Destination);
            this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, Destination.x , Speed * Time.deltaTime), Mathf.Lerp(this.transform.position.y, Destination.y , Speed * Time.deltaTime), 0);
            //this.transform.localPosition = new Vector3(Mathf.Lerp(this.transform.localPosition.x, Destination.x, Speed * Time.deltaTime), Mathf.Lerp(this.transform.localPosition.y, Destination.y, Speed * Time.deltaTime), 0);
            if (Mathf.Abs(this.transform.position.x - Destination.x) <= 20f &&  Mathf.Abs(this.transform.position.y - Destination.y) <= 20f)
            {
                this.transform.position = Destination;
                moving = false;
                if (this.GetComponent<BattleCardDrag>().DropGoBack)
                {
                    CardsLayoutManager.instance.CardGoBack--;
                    this.GetComponent<BattleCardDrag>().DropGoBack = false;

                }
            }
        }
    }

    public void Move(Vector3 destination) 
    {
        Destination = destination;
        moving = true;
    }
    */

    public void call_Move(Vector3 ori_pos, Vector3 des_pos)
    {
        //Debug.LogFormat("±q{0} ¨ì {1}", ori_pos, des_pos);
        //original_pos= ori_pos;
        //destination_pos= des_pos;
        StopAllCoroutines();
        StartCoroutine(Move(ori_pos, des_pos));
    }

    public void stop_Move()
    {
        StopAllCoroutines();
        
    }

    IEnumerator Move(Vector3 ori_pos, Vector3 des_pos)
    {
        float workTime = 0;
        moving = true;
        while (moving)
        {
            workTime += Time.deltaTime * 5;
            transform.position = Vector3.Lerp(ori_pos, des_pos, workTime);

            if (workTime >= 1)
            {
                transform.position = des_pos;
                moving= false;
            }
            yield return null;
        }
    }
}

