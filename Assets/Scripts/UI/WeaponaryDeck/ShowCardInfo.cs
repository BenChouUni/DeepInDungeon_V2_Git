using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowCardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Card_prefab;
    GameObject card;

    /*
    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(card);
    }
    */
    public void OnPointerEnter(PointerEventData eventData)
    {
        card = Instantiate(Card_prefab, this.transform, false);
        card.GetComponent<CardDisplay>().CardData = this.GetComponent<CardDisplay>().CardData;
        card.transform.position = new Vector3(960 - 150, 540, 0);
        card.transform.localScale = Vector2.one * 2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(card);
    }
}
