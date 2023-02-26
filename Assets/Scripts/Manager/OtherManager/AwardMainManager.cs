using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AwardMainManager : MonoBehaviour, IDataPersistence
{
    public CardDataBase CardList;
    public GameObject CardPrefab;
    public int CardNum;
    public Transform Panel;
    public int MainID;
    public int SupportID;
    public PlayerData Data;

    public int MainCardNum;
    public int SupportCardNum;

    public List<int> AwardCardList= new List<int>();

    public void LoadData(GameData data)
    {
        Data = data.playerData;

    }
    public void SaveData(ref GameData data)
    {

    }
    void Start()
    {
        SupportCardNum = CardNum - MainCardNum;
        MainID = Data.MainWeaponData.id;
        SupportID = Data.SupportWeaponData.id;
        Panel.GetComponent<RectTransform>().sizeDelta = new Vector2((CardNum * 300) + (CardNum + 1) * 50f, 500f);
        int[] IDList = new int[CardNum];
        IDList = RandomWeapon(CardNum);
        for(int i = 0; i < CardNum; i++)
        {
            //Debug.Log(IDList[i]);
            if (IDList[i] == 0)
            {
                CreateCardOnPanel(RandomCardID(MainID));
                //RandomCardID(MainID);
            }
            else
            {
                CreateCardOnPanel(RandomCardID(SupportID));
            }
        }

    }


    /// <summary>
    /// 隨機武器id
    /// </summary>
    /// <param name="cardNum"></param>
    /// <returns></returns>
    public int[] RandomWeapon(int cardNum)
    {
        int[] randomidlist = new int[cardNum];
        int maincardnum = 0;
        int supportcardnum = 0;
        for (int i = 0; i < cardNum; i++)
        {
            
            int randomid = Random.Range(1, 3) % 2;
            if (maincardnum < MainCardNum && randomid == 0)
            {
                randomidlist[i] = randomid;
                maincardnum++;
                //Debug.Log(randomid);

            }
            else if (supportcardnum < SupportCardNum && randomid == 1)
            {
                supportcardnum++;
                randomidlist[i] = randomid;
                //Debug.Log(randomid);
            }
            else
            {
                i--;
            }
        }
        return randomidlist;
    }

    /// <summary>
    /// 隨機卡牌id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int RandomCardID(int id)
    {
        int weaponcardnum = 0;
        int start = 0;
        foreach (CardSO item in CardList.cardList)
        {
            if((int)(item.cardData.id / 100) == id)
            {
                start = 1;
                weaponcardnum++;
            }
            else if(start == 1)
            {
                break;
            }
        }
        int randomid = Random.Range(0, weaponcardnum);
        int ID = id * 100 + randomid;
        if (GetCardData(ID).initialnum == 0)    //過濾初始牌
        {
            foreach (int item in AwardCardList)    //避免重複牌
            {
                if (ID == item)
                {
                    ID = RandomCardID(id);
                }
            }
            AwardCardList.Add(ID);
        }
        else
        {
            ID = RandomCardID(id);
        }
        return ID;
    }

    public void CreateCardOnPanel(int id)
    {
        CardData cardData = GetCardData(id);
        GameObject new_card;

        if (cardData != null)
        {
            new_card = Instantiate(CardPrefab, Panel, false);
            new_card.GetComponent<CardDisplay>().CardData = cardData;
        }
    }

    public CardData GetCardData(int id)
    {
        foreach (CardSO item in CardList.cardList)
        {
            if (item.cardData.id == id)
            {
                return item.cardData;
            }
        }
        return null;
    }
}
