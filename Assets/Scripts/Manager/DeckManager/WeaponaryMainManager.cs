using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponaryMainManager : MonoSingleton<WeaponaryMainManager>
{
    //drop
    private bool hasMainWeapon;
    private bool hasSupportWeapon;
    //weapon dropzone
    public WeaponDropZone mainWeaponDropZone;
    public WeaponDropZone supportWeaponDropZone;
    public WeaponDropZone weaponList;
    public Transform weaponListPanel;//drop地方跟回去parent不一樣
    private WeaponDropZoneType DropZoneFrom;
    //drag
    [SerializeField]
    private GameObject OnDragGO;
    public bool isDrag; //是否正有東西被拖拽

    private void Awake()
    {
        weaponList.SetZoneType(WeaponDropZoneType.InList);
        mainWeaponDropZone.SetZoneType(WeaponDropZoneType.MainWeapon);
        supportWeaponDropZone.SetZoneType(WeaponDropZoneType.SupportWeapon);
        
    }
    void Start()
    {
        hasMainWeapon = hasSupportWeapon = isDrag = false;

        PlayerData playerData = PlayerDataManager.instance.playerData;
        //PlayerDataManager.instance.ShowPlayerData();
        WeaponStoreManager.instance.InitialWeaponStore();
        //用weaponName判斷，避免全空
        if (playerData.MainWeaponData.weaponName != "")
        {
            WeaponData data = playerData.MainWeaponData;
            GameObject weaponObj = FindWeaponOnList(data.id);
            PutInDropZone(mainWeaponDropZone, data, WeaponDropZoneType.InList, weaponObj);
        }

        if (playerData.SupportWeaponData.weaponName != "")
        {
            WeaponData data = playerData.SupportWeaponData;
            GameObject weaponObj = FindWeaponOnList(data.id);
            PutInDropZone(supportWeaponDropZone, data, WeaponDropZoneType.InList, weaponObj);
        }
        
        Debug.Log("ShowPlayerData");


    }

    public void StartBattle()
    {
        SceneManager.LoadScene(1);
        DataPersistenceManager.instance.SaveGame();
    }
    /// <summary>
    /// 通知管理器現在什麼物件被拖動
    /// </summary>
    public void StartDrag(GameObject gameObject,WeaponDropZoneType dropZone)
    {
        isDrag = true;
        this.OnDragGO = gameObject;
        this.DropZoneFrom = dropZone;
        
    }
    /// <summary>
    /// onDragGO以及DropZoneFron先不改，可能drop要用到
    /// </summary>
    public void EndDrag()
    {
        isDrag = false;
        this.OnDragGO = null;
        
    }
    /// <summary>
    /// drop 武器進入時 呼叫
    /// </summary>
    /// <param name="dropZone"></param>
    public void WeaponDropRequest(WeaponDropZone dropZone)
    {
        if (OnDragGO == null)
        {
            Debug.Log("現在並沒有拖拽物品，可能是搖桿觸發的判定");
            return;
        }
        
        //check if weapon確認是否是武器
        if (OnDragGO.TryGetComponent(out WeaponDisplay weaponDisplay) == false)
        {
            return;
        }
        //放入武器物件事的基本資料跟類型，由Ondrag來決定
        WeaponData data = OnDragGO.GetComponent<WeaponDisplay>().WeaponData;
        //新來武器原先所在的dropzone
        WeaponDropZoneType typeFrom = OnDragGO.GetComponent<DragCard>().currentDropZoneType;

        //要放入的GO
        GameObject putInWeapon = OnDragGO;
        //放入
        PutInDropZone(dropZone, data, typeFrom, putInWeapon);
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dropZone">放置區域本身</param>
    /// <param name="data">武器資料</param>
    /// <param name="typeFrom">從哪塊區域來</param>
    /// <param name="putInWeapon">武器物件</param>
    private void PutInDropZone(WeaponDropZone dropZone, WeaponData data, WeaponDropZoneType typeFrom, GameObject putInWeapon)
    {
        //dropzone的類型
        WeaponDropZoneType zoneType = dropZone.dropZoneType;
        //武器類型
        WeaponType weaponType = data.weaponType;

        //如果同處放下則不致行
        if (typeFrom == zoneType)
        {
            Debug.Log("原地放下");
            return;
        }

        //判斷武器類型跟放置區域的關係
        if (!IsWeaponFitZone(weaponType,zoneType))
        {
            Debug.Log("武器不能放置在此區域");
            return;
        }

        //如果放入武器庫
        if (zoneType == WeaponDropZoneType.InList)
        {
            Debug.Log("放入武器庫");
            dropZone.PutInWeapon(putInWeapon, weaponListPanel);
            //移除原先武器所在位置的資料


            //PlayerDataManager.instance.RemoveWeapon(typeFrom);
            //DeckManager.instance.RemoveCardsByType(typeFrom);
            ReleaseDropZone(typeFrom);

        }
        else //如果放入主武器輔助武器區域
        {
            PutInHands(dropZone, zoneType, data, typeFrom, putInWeapon);
        }
    }

    /// <summary>
    /// 放入手中
    /// </summary>
    /// <param name="dropZone"></param>
    /// <param name="type"></param>
    /// <param name="data"></param>
    /// <param name="typeFrom"></param>
    /// <param name="putInWeapon"></param>
    private void PutInHands(WeaponDropZone dropZone, WeaponDropZoneType type, WeaponData data, WeaponDropZoneType typeFrom, GameObject putInWeapon)
    {
        //如果區域上面是滿的
        if (dropZone.isFull)
        {
            
            //將上面武器卡牌物件跟新來卡牌的dropzone區域交換
            WeaponDropZone ExchangeZone = GetDropZoneByType(typeFrom);//交換要去的dropzone

            //原本在此dropzone上的物件
            GameObject weaponOn = dropZone.GetWeaponOn();
            //要被交換的武器資料
             WeaponData exchangeWeaponData = weaponOn.GetComponent<WeaponDisplay>().WeaponData;
            //一定會先移除佔位的武器，看要放哪
            ReleaseDropZone(typeFrom);
            //DeckManager.instance.RemoveCardsByType(type);

            Debug.LogFormat("上面是滿的有{0}", exchangeWeaponData.weaponName);

            //跟上方的武器庫交換
            if (typeFrom == WeaponDropZoneType.InList)
            {
                Debug.LogFormat("把{0}送回list",exchangeWeaponData.weaponName);
                ExchangeZone.PutInWeapon(weaponOn, weaponListPanel);//

            }
            else //左右交換
            {
                
                
                //檢測能否交換
                if (IsWeaponFitZone(exchangeWeaponData.weaponType, typeFrom))
                {
                    Debug.Log("左右交換");
                    ExchangeZone.PutInWeapon(weaponOn);

                    PlayerDataManager.instance.SetWeapon(typeFrom, exchangeWeaponData);
                    CreateDeckByWeapon(exchangeWeaponData.id, typeFrom);

                }
                else
                {
                    Debug.Log("不能左右交換");
                    //原本武器回去list
                    weaponList.PutInWeapon(weaponOn, weaponListPanel);
                }

            }


        }
        else if (typeFrom != WeaponDropZoneType.InList)//若從主副武器區地方放入但上面是空的
        {
            Debug.Log("從主副武器區地方放入但上面是空的");
            PlayerDataManager.instance.RemoveWeapon(typeFrom);
            DeckManager.instance.RemoveCardsByType(typeFrom);
            ReleaseDropZone(typeFrom);
        }
        dropZone.PutInWeapon(putInWeapon);
        CreateDeckByWeapon(data.id, type);
        PlayerDataManager.instance.SetWeapon(type, data);
        //dropZone.isFull = true;
    }


    
    private void ReleaseDropZone(WeaponDropZoneType type)
    {
        
        if (type != WeaponDropZoneType.InList)
        {
            if (type == WeaponDropZoneType.MainWeapon)
            {
                mainWeaponDropZone.RemoveWeaponOn();
                mainWeaponDropZone.isFull = false;
            }
            else if (type == WeaponDropZoneType.SupportWeapon)
            {
                supportWeaponDropZone.RemoveWeaponOn();
                supportWeaponDropZone.isFull = false;
            }

            PlayerDataManager.instance.RemoveWeapon(type);
            DeckManager.instance.RemoveCardsByType(type);
        }
        else
        {
            Debug.Log("武器庫被釋放，可能有問題");
            
        }
    }

    private GameObject FindWeaponOnList(int weaponID)
    {
        foreach (Transform item in weaponListPanel)
        {
            if (item.GetComponent<WeaponDisplay>().WeaponData.id == weaponID)
            {
                return item.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// 根據放入武器創建卡牌
    /// </summary>
    /// <param name="weaponId"></param>
    private void CreateDeckByWeapon(int weaponId,WeaponDropZoneType type)
    {
        //如果要創建的卡牌已經有了，則移除
        if (! DeckManager.instance.CheckEmpty(type))
        {
            DeckManager.instance.RemoveCardsByType(type);
        }
        List<int> cardIDs = WeaponToCardConverter.instance.WeaponIdToCardId(weaponId);

        foreach (int cardId in cardIDs)
        {
            CardData data = DeckManager.instance.GetCardDataByID(cardId);

            if (type == WeaponDropZoneType.MainWeapon)
            {
                data.SetWeaponData(PlayerDataManager.instance.playerData.MainWeaponData);
            }
            else if (type == WeaponDropZoneType.SupportWeapon)
            {
                data.SetWeaponData(PlayerDataManager.instance.playerData.SupportWeaponData);
            }
            
            if(data.initialnum != 0)
            {
                DeckManager.instance.CreateCardOnPanel(data, type);
            }
        }
        
    }
    //根據dropzonetype 給出 dropZone
    private WeaponDropZone GetDropZoneByType(WeaponDropZoneType type)
    {
        switch (type)
        {
            case WeaponDropZoneType.InList:
                return weaponList;
            case WeaponDropZoneType.MainWeapon:
                return mainWeaponDropZone;
            case WeaponDropZoneType.SupportWeapon:
                return supportWeaponDropZone;
            default:
                return null;
        }
    }

    //判斷武器類型跟放置區域的關係
    private bool IsWeaponFitZone(WeaponType weaponType,WeaponDropZoneType zoneType)
    {
        if (weaponType == WeaponType.Both || zoneType == WeaponDropZoneType.InList)
        {
            return true;
        }
        else if (weaponType == WeaponType.Main)
        {
            if (zoneType == WeaponDropZoneType.SupportWeapon)
            {
                return false;
            }
            else return true;
        }
        else 
        {
            if (zoneType == WeaponDropZoneType.MainWeapon)
            {
                return false;
            }
            else return true;
        }
    }
}
