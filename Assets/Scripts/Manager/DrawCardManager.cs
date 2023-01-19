using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawCardManager
{
    public class DrawCardManager : MonoBehaviour
    {

        public List<CardSO> CardDeck;
        public GameObject CardPrefab;
        public Transform HandCardPanel;
        public List<Transform> HandCardList;
        public List<Vector3> TargetPosition;
        public List<Quaternion> TargetRotation;

        public void Click()
        {
            Draw_Card_On_Hand(1);
            Debug.Log(HandCardList.Count);
        }

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
        }

        public void SetLayout()
        {
            if(HandCardList.Count > 2)
            {
                SetCircleLayout();
            }
            else
            {
                SetLineLayout();
            }
        }

        public void SetCircleLayout()
        {
            Debug.Log("SetCircleLayout");
            TargetPosition.Clear();
            TargetRotation.Clear();
            float startAngle = Mathf.PI * 105f / 180f;
            float endAngle = Mathf.PI * 75f / 180f;
            float radius = 800f;
            /*
            for(int i = 0 ; i < HandCardList.Count; i++)
            {
                float angle = Mathf.Lerp(startAngle, endAngle, i / HandCardList.Count - 1f);
                TargetPosition.Add(new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius - radius - 250f, 1f);
                TargetRotation.Add(Quaternion(0f, Mathf.Lerp(15f, -15f, i / (HandCardList.Count - 1f)), 0f));
            }
            */
            
        }

        public void SetLineLayout()
        {
            Debug.Log("SetLineLayout");
            TargetPosition.Clear();
            TargetRotation.Clear();
            float positionX = (1f - HandCardList.Count) * 100f / 2f;
            if(HandCardList.Count == 1)
            {
                TargetPosition.Add(new Vector3(960f, 250f, 1f));
                TargetRotation.Add(Quaternion.Euler(Vector3.zero));
                //HandCardList[0].position = TargetRotation[0] * TargetPosition[0];
                Debug.Log(HandCardList[0]);
            }
            else
            {
                for(int i = 0; i < HandCardList.Count; i++)
                {
                    TargetPosition.Add(new Vector3(Mathf.Lerp(positionX, -positionX, i / (HandCardList.Count - 1f)) + 960f, 250f, 1f));
                    TargetRotation.Add(Quaternion.Euler(Vector3.zero));
                    //HandCardList[0].position = TargetRotation[i] * TargetPosition[i];
                    Debug.Log(HandCardList[i]);
                }
            }
        }
    }
}
