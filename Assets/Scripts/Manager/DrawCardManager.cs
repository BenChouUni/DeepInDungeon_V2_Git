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

        }

        public void SetLineLayout()
        {
            TargetPosition.Clear();
            TargetRotation.Clear();
            float positionX = (1f - HandCardList.Count) * 100f / 2f;
            if(HandCardList.Count == 1)
            {
                TargetPosition.Add(new Vector3(0f, -0.3f, 1f));
                TargetRotation.Add(Quaternion.Euler(Vector3.zero));
            }
            else
            {
                for(int i = 0; i < HandCardList.Count; i++)
                {
                    TargetPosition.Add(new Vector3(Mathf.Lerp(positionX, -positionX, i / (HandCardList.Count - 1f)), -0.3f, 1f));
                    TargetRotation.Add(Quaternion.Euler(Vector3.zero));
                }
            }
        }
    }
}
