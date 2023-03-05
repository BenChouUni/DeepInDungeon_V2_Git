using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMoveUI : MonoBehaviour
{
    public float Speed = 10f;
    public bool moving = false;
    public Vector3 Destination;
    void Update()
    {
        if (moving)
        {
            this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, Destination.x , Speed * Time.deltaTime), Mathf.Lerp(this.transform.position.y, Destination.y , Speed * Time.deltaTime), 0);
            //this.transform.localPosition = new Vector3(Mathf.Lerp(this.transform.localPosition.x, Destination.x, Speed * Time.deltaTime), Mathf.Lerp(this.transform.localPosition.y, Destination.y, Speed * Time.deltaTime), 0);
            if (Mathf.Abs(this.transform.position.x - Destination.x) <= 20f &&  Mathf.Abs(this.transform.position.y - Destination.y) <= 20f)
            {
                this.transform.position = Destination;
                moving = false;
            }
        }
    }

    public void Move(Vector3 destination) 
    {
        Destination = destination;
        moving = true;
    }
}

