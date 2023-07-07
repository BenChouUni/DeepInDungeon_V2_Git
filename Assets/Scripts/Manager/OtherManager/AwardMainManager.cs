using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class AwardMainManager : MonoSingleton<AwardMainManager>, IDataPersistence
{
    public GameObject AwardScene;

    public CardDataBase CardList;
    public GameObject CardPrefab;
    public int CardNum;
    public int AwardNum;
    public Transform Panel;
    public int MainID;
    public int SupportID;
    public GameData Data;
    public UnityEngine.UI.Text Text;
    public GameObject next_level_button;
    public int MainCardNum;
    public int SupportCardNum;

    public List<int> AwardCardList= new List<int>();

    public bool Choosen = false;

    public void LoadData(GameData data)
    {
        Data = data;

    }
    public void SaveData(ref GameData data)
    {
    }
    
    void Start()
    {
        Text.GetComponent<UnityEngine.UI.Text>().text = "請選擇" + AwardNum + "張牌作為獎勵";
        SupportCardNum = CardNum - MainCardNum;
        MainID = Data.playerData.MainWeaponData.id;
        SupportID = Data.playerData.SupportWeaponData.id;
        ChangePanel();
        int[] IDList = new int[CardNum];
        IDList = RandomWeapon(CardNum);
        for(int i = 0; i < CardNum; i++)
        {
            if (IDList[i] == 0)
            {
                CreateCardOnPanel(RandomCardID(MainID));
            }
            else
            {
                CreateCardOnPanel(RandomCardID(SupportID));
            }
        }

    }


    private void ChangePanel()
    {
        Panel.GetComponent<RectTransform>().sizeDelta = new Vector2((CardNum * 300) + (CardNum + 1) * 50f, 500f);
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
        int flag = 1;
        int randomid;
        int ID = 0;
        while (flag == 1)
        {
            randomid = Random.Range(0, weaponcardnum);
            ID = id * 100 + randomid;
            if (GetCardData(ID).initialnum == 0)    //過濾初始牌
            {
                foreach (int item in AwardCardList)    //避免重複牌
                {
                    if (ID == item)
                    {
                        flag = 0;
                        break;
                    }
                }
                if (flag == 1)
                {
                    break;
                }
                else
                {
                    flag = 1;
                }
            }
        }
        AwardCardList.Add(ID);
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


    public void AddCardToDeck(CardData _carddata)
    {
        if (Data.playerData.MainWeaponData.id == (_carddata.id / 100))
        {

            Data.mainWeaponDeck.Add(_carddata);
        }
        else
        {
            Data.supWeaponDeck.Add(_carddata);
        }
        AwardNum--;
        if(AwardNum <= 0)
        {
            Text.GetComponent<UnityEngine.UI.Text>().text = "已選擇獎勵，請繼續前進!";
            Choosen = true;
            next_level_button.SetActive(true);
        }
        else
        {
            Text.GetComponent<UnityEngine.UI.Text>().text = "請選擇" + AwardNum + "張牌作為獎勵";
        }
        CardNum -= 1;
        ChangePanel();
    }

    public void ShowAwardScene()
    {
        AwardScene.SetActive(true);
    }

    public void Next_Level()
    {
        AwardScene.SetActive(false);
        next_level_button.SetActive(false);
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(4);
    }
}
