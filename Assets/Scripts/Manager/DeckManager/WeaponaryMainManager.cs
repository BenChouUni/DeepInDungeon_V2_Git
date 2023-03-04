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
        PlayerDataManager.instance.ShowPlayerData();
        WeaponStoreManager.instance.InitialWeaponStore();
        //用weaponName判斷，避免全空
        if (playerData.MainWeaponData.weaponName != "")
        {
            WeaponData data = playerData.MainWeaponData;
            GameObject weaponObj = FindWeaponOnList(data.id);
            PutInDropZone(mainWeaponDropZone, WeaponDropZoneType.MainWeapon, data, WeaponDropZoneType.InList, weaponObj);
        }

        if (playerData.SupportWeaponData.weaponName != "")
        {
            WeaponData data = playerData.SupportWeaponData;
            GameObject weaponObj = FindWeaponOnList(data.id);
            PutInDropZone(supportWeaponDropZone, WeaponDropZoneType.SupportWeapon, data, WeaponDropZoneType.InList, weaponObj);
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
        
    }
    /// <summary>
    /// drop 武器進入時 呼叫
    /// </summary>
    /// <param name="dropZone"></param>
    public void WeaponDropRequest(WeaponDropZone dropZone)
    {
        //dropzone的類型
        WeaponDropZoneType type = dropZone.dropZoneType;

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
        PutInDropZone(dropZone, type, data, typeFrom, putInWeapon);
    }

    

    private void PutInDropZone(WeaponDropZone dropZone, WeaponDropZoneType type, WeaponData data, WeaponDropZoneType typeFrom, GameObject putInWeapon)
    {
        //如果同處放下則不致行
        if (typeFrom == type)
        {
            Debug.Log("原地放下");
            return;
        }

        //如果放入武器庫
        if (type == WeaponDropZoneType.InList)
        {
            Debug.Log("放入武器庫");
            dropZone.PutInWeapon(putInWeapon, weaponListPanel);
            //移除原先武器所在位置的資料


            PlayerDataManager.instance.RemoveWeapon(typeFrom);
            DeckManager.instance.RemoveCardsByType(typeFrom);
            ReleaseDropZone(typeFrom);

        }
        else //如果放入主武器輔助武器區域
        {
            //如果區域上面是滿的
            if (dropZone.isFull)
            {
                Debug.Log("上面是滿的");
                //將上面武器卡牌物件跟新來卡牌的dropzone區域交換
                WeaponDropZone ExchangeZone = GetDropZoneByType(typeFrom);//交換要去的dropzone

                //原本在此dropzone上的物件
                GameObject weaponOn = dropZone.weaponOn[0];
                dropZone.weaponOn.RemoveAt(0);
                DeckManager.instance.RemoveCardsByType(type);

                //跟上方的武器庫交換
                if (typeFrom == WeaponDropZoneType.InList)
                {
                    
                    ExchangeZone.PutInWeapon(weaponOn, weaponListPanel);//

                }
                else //左右交換
                {
                    Debug.Log("左右交換");
                    WeaponData weaponData = weaponOn.GetComponent<WeaponDisplay>().WeaponData;
                    ExchangeZone.PutInWeapon(weaponOn);
                    
                    PlayerDataManager.instance.SetWeapon(typeFrom, weaponData);
                    CreateDeckByWeapon(weaponData.id, typeFrom);

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
            dropZone.isFull = true;
        }
    }
    //將dropzone當中的fulle改成false
    private void ReleaseDropZone(WeaponDropZoneType type)
    {
        switch (type)
        {
            case WeaponDropZoneType.InList:
                return;
            case WeaponDropZoneType.MainWeapon:
                mainWeaponDropZone.isFull = false;
                return;
            case WeaponDropZoneType.SupportWeapon:
                supportWeaponDropZone.isFull = false;
                return;
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
        if (! DeckManager.instance.CheckEmpty(type))
        {
            DeckManager.instance.RemoveCardsByType(type);
        }
        List<int> cardIDs = WeaponToCardConverter.instance.WeaponIdToCardId(weaponId);

        foreach (int cardId in cardIDs)
        {
            CardData data = DeckManager.instance.GetCardDataByID(cardId);
            /*
            if(data.initialnum != 0)
            {
                DeckManager.instance.CreateCardOnPanel(data, type);
            }
            */
            
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
}
